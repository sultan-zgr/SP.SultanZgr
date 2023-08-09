using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Business.Abstract;
using SP.Business.MailService;
using System.Data;

namespace SP.API.Controller
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public MailController(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

       
    }
}