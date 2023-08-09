using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Base.Enums.Months;
using SP.Business.Abstract;
using SP.Business.Concrete;
using SP.Schema;
using SP.Schema.Request;
using SP.Schema.Response;
using System.Data;
using System.Security.Claims;

namespace SP.API.Controller
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyInvoiceController : ControllerBase
    {
        private readonly IMonthlyInvoiceService _monthlyInvoiceService;
        private readonly IUserService _userService;

        public MonthlyInvoiceController(IMonthlyInvoiceService monthlyInvoiceService, IUserService userService)
        {
            _monthlyInvoiceService = monthlyInvoiceService;
            _userService = userService;
        }

        [HttpGet]

        public async Task<ApiResponse<List<MonthlyInvoiceResponse>>> GetAllMonthlyInvoices()
        {

            var response = await _monthlyInvoiceService.GetAll();
            return response;
        }

        [HttpGet("{id}")]

        public async Task<ApiResponse<MonthlyInvoiceResponse>> GetMonthlyInvoiceById(int id)
        {

            var response = await _monthlyInvoiceService.GetById(id);
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse<MonthlyInvoiceResponse>> AddMonthlyInvoiceByMonth([FromBody] MonthlyInvoiceRequest request)
        {
           // Kullanıcı veritabanımda kayıtlı mı?
                var userExists = await _userService.CheckUserExists(request.UserId);

                if (!userExists)
                {
                    return new ApiResponse<MonthlyInvoiceResponse>("Kullanıcı bulunamadı.");
                }

                var response = await _monthlyInvoiceService.AddMonthlyInvoiceByMonth(request, request.Months);
                return response;
        }



        [HttpPut("{id}")]

        public async Task<ApiResponse<MonthlyInvoiceResponse>> UpdateMonthlyInvoiceByMonth(int id, [FromBody] MonthlyInvoiceRequest request)
        {
            var userExists = await _userService.CheckUserExists(request.UserId);

            if (!userExists)
            {
                return new ApiResponse<MonthlyInvoiceResponse>("Kullanıcı bulunamadı.");
            }
            var response = await _monthlyInvoiceService.UpdateMonthlyInvoiceByMonth(id, request, request.Months);
            return response;
        }

        [HttpDelete("{id}")]

        public async Task<ApiResponse> DeleteMonthlyInvoice(int id)
        {
            var response = await _monthlyInvoiceService.Delete(id);
            return response;
        }

        [HttpGet("unpaid-users")]
        public async Task<ActionResult<ApiResponse<List<MonthlyInvoiceResponse>>>> GetUnpaidUsers()
        {
            var unpaidUsersResponse = await _monthlyInvoiceService.GetUsersWithUnpaidInvoicesAsync(); // Bu metodu IUserService içine eklemeniz gerekiyor.

            return Ok(unpaidUsersResponse);
        }

    }
}

