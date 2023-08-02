using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SP.Business.Token;
using SP.Data;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Request.Token;
using SP.Schema.Response.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SP.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AppUserController(ITokenService tokenService, UserManager<User> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost("adminlogin")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password) && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var token = await _tokenService.GenerateTokenAsync(user, "Admin");
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [HttpPost("userlogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password) && await _userManager.IsInRoleAsync(user, "User"))
            {
                var token = await _tokenService.GenerateTokenAsync(user, "User");
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}