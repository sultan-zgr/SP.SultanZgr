using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.Token;
using SP.Schema;
using SP.Schema.Request;
using System.Security.Cryptography;
using System.Text;

namespace SP.API.Controller
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

     
        [HttpPost]
        public async Task<ApiResponse> UserCreate([FromBody] UserRequest request)
        {
            string hashedPassword = CalculateMD5Hash(request.Password);
            request.Password = hashedPassword;

            var response = await _service.Insert(request); 

            return response;
        }
        private string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserResponse>>>> GetAllUsers()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> UpdateUser(int id, [FromBody] UserRequest request)
        {

            string hashedPassword = CalculateMD5Hash(request.Password);
            request.Password = hashedPassword;

            var response = await _service.Update(id, request);
            return response;




        }
    }

}