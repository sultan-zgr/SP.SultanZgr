using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Schema.Request;
using SP.Schema.Response;
using System.Data;

namespace SP.API.Controller
{

    [Authorize(Roles = "user")]
    [ApiController]
    [Route("sipy/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<ApiResponse<List<UserResponse>>> GetAll()
        {
            var response = service.GetAll();
            return await response;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<UserResponse>> Get(int id)
        {
            var response = service.GetById(id);
            return await response;
        }


        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] UserRequest request)
        {
            var response = service.Insert(request);
            return await response;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] UserRequest request)
        {

            var response = service.Update(id, request);
            return await response;
        }


        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var response = service.Delete(id);
            return await response;
        }

    }
}