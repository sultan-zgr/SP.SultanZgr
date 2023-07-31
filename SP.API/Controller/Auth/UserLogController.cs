using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Schema.Request.UserLog;
using SP.Schema.Response.UserLog;
using System.Security.Claims;

namespace SP.API.Controller.Auth
{
    [Authorize]
    [ApiController]
    [Route("sipy/api/[controller]")]
    public class UserLogController : ControllerBase
    {
        private readonly IUserLogService service;
        public UserLogController(IUserLogService service)
        {
            this.service = service;
        }


        [HttpGet]
        public ApiResponse<List<UserLogResponse>> GetAll()
        {
            var username = User.Claims.Where(x => x.Type == "UserName")?.FirstOrDefault();
            var userid = (User.Identity as ClaimsIdentity).FindFirst("UserId")?.Value;
            var response = service.GetByUserSession(username?.Value);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<UserLogResponse>> Get(int id)
        {
            var response = service.GetById(id);
            return await response;
        }


        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] UserLogRequest request)
        {
            var response = service.Insert(request);
            return await response;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] UserLogRequest request)
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

