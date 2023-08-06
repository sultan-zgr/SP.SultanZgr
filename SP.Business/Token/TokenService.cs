using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SP.Base.BaseResponse;
using SP.Base.JWT;
using SP.Base.LogType;
using SP.Business.Abstract;
using SP.Data;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Token
{
    public class TokenService : ITokenService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IUserLogService userLogService;
        private readonly JwtConfig jwtConfig;
        public TokenService(IUnitOfWork unitOfWork, IUserLogService userLogService, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.unitOfWork = unitOfWork;
            this.userLogService = userLogService;
            this.jwtConfig = jwtConfig.CurrentValue;
        }

        public ApiResponse<TokenResponse> CreateToken(TokenRequest request)
        {
            if (request is null)
            {
                return new ApiResponse<TokenResponse>("Request was null");
            }
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return new ApiResponse<TokenResponse>("Request was null");
            }

            request.UserName = request.UserName.Trim().ToLower();
            request.Password = request.Password.Trim();


            var user = unitOfWork.DynamicRepo<User>().Where(x => x.UserName.Equals(request.UserName)).FirstOrDefault();
            if (user is null)
            {
                Log(request.UserName, LogType.InValidUserName);
                return new ApiResponse<TokenResponse>("Invalid user informations");
            }
            if (user.Password.ToLower() != CreateMD5(request.Password))
            {
                user.PasswordRetryCount++;
                

                if (user.PasswordRetryCount > 3)
                    user.Status = 2;

                unitOfWork.DynamicRepo<User>().UpdateAsync(user);
                unitOfWork.SaveChangesAsync();

                Log(request.UserName, LogType.WrongPassword);
                return new ApiResponse<TokenResponse>("Invalid user informations");
            }


            if (user.Status != 1)
            {
                Log(request.UserName, LogType.InValidUserStatus);
                return new ApiResponse<TokenResponse>("Invalid user status");
            }
            if (user.PasswordRetryCount > 3)
            {
                Log(request.UserName, LogType.PasswordRetryCountExceded);
                return new ApiResponse<TokenResponse>("Password retry count exceded");
            }

           
            user.Status = 1;


            unitOfWork.DynamicRepo<User>().UpdateAsync(user); 
            unitOfWork.SaveChangesAsync();


            string token = Token(user);

            Log(request.UserName, LogType.LogedIn);

            TokenResponse response = new()
            {
                AccessToken = token,
                ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                UserName = user.UserName
            };

            return new ApiResponse<TokenResponse>(response);
        } 
        private Claim[] GetClaims(User user)
        {
            var claims = new[]
            {
            new Claim("UserName",user.UserName),
            new Claim("UserId",user.UserId.ToString()),
            new Claim("Role",user.Role),
            new Claim("Status",user.Status.ToString()),
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.Name,$"{user.FullName}")
        };

            return claims;
        }
        private string Token(User user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
                var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }


       

        private void Log(string username, string logType)
        {
            UserLogRequest request = new()
            {
                LogType = logType,
                UserName = username,
                PaymentDate = DateTime.UtcNow
            };
            userLogService.Insert(request);
        }

        public string CreateMD5(string input)  //ŞİFRELEME
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();

            }
        }
    }
}
