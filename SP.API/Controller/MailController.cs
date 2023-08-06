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

        [HttpGet("send-reminders")]
        public async Task<IActionResult> SendPaymentReminders()
        {
            try
            {
                var response = await _userService.GetUsersWithPendingPayments();
                if (response.Success && response.Response != null)
                {
                    foreach (var user in response.Response)
                    {
                        await _mailService.SendReminderEmail(user.Email);
                    }
                    return Ok("Payment reminders sent successfully!");
                }
                else
                {
                    return BadRequest("Failed to get users with pending payments.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}