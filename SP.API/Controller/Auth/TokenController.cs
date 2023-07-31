
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Token;
using SP.Schema.Request.Token;
using SP.Schema.Response.Token;
using SP.Service;

namespace SP.API.Controller.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly ITokenService service;
        public TokenController(ITokenService service)
        {
            this.service = service;
        }


        [TypeFilter(typeof(LogResourceFilter))]
        [TypeFilter(typeof(LogActionFilter))]
        [TypeFilter(typeof(LogAuthorizationFilter))]
        [TypeFilter(typeof(LogResultFilter))]
        [TypeFilter(typeof(LogExceptionFilter))]
        [HttpGet("HeartBeat")]
        public ApiResponse Get()
        {
            return new ApiResponse("Hello");
        }


        [HttpPost("Login")]
        public ApiResponse<TokenResponse> Post([FromBody] TokenRequest request)
        {
            var response = service.Login(request);
            return response;
        }
    }
}
