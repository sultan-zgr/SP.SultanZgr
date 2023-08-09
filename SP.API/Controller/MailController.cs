using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.MailService;
using SP.Schema;
using System.Data;

namespace SP.API.Controller
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMonthlyInvoiceService _invoiceService;
        private readonly IMailService _mailService;

        public MailController(IMailService mailService, IMonthlyInvoiceService invoiceService)
        {
            _mailService = mailService;
            _invoiceService = invoiceService;
        }

        [HttpGet("send-reminders")]
        public async Task<ApiResponse<UserResponse>> SendReminders()
        {
                var unpaidUsersResponse = await _invoiceService.GetUsersWithUnpaidInvoicesAsync();

                if (unpaidUsersResponse.Success)
                {
                    foreach (var userResponse in unpaidUsersResponse.Response)
                    {
                        await _mailService.SendReminderEmail(userResponse.Email);
                    return new ApiResponse<UserResponse>("Your mail has been sent.");
                }

                }

            return new ApiResponse<UserResponse>("No unpaid invoices found.");

        }
    }
    }
