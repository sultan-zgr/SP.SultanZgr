using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.Token;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;
using System.Data;

namespace SP.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class AppUserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        

        public AppUserController(ITokenService tokenService, IUserService service)
        {
            _tokenService = tokenService;
         
        }

        [HttpPost("AdminLogin")]
        public ApiResponse<TokenResponse> AdminLogin([FromBody] TokenRequest request)
        {
            // You can implement admin login logic here
            var response = _tokenService.CreateToken(request);
            return response;
        }

        [HttpPost("UserLogin")]
        public ApiResponse<TokenResponse> UserLogin([FromBody] TokenRequest request)
        {
            // You can implement user login logic here
            var response = _tokenService.CreateToken(request);
            return response;
        }

    }
}