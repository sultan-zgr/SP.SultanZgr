using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.Concrete;
using SP.Schema.Request;
using SP.Schema.Response;
using System.Security.Claims;

namespace SP.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyInvoiceController : ControllerBase
    {
        private readonly IMonthlyInvoiceService _monthlyInvoiceService;

        public MonthlyInvoiceController(IMonthlyInvoiceService monthlyInvoiceService)
        {
            _monthlyInvoiceService = monthlyInvoiceService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<List<MonthlyInvoiceResponse>>>> GetAllMonthlyInvoices()
        {
            var response = await _monthlyInvoiceService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<MonthlyInvoiceResponse>>> GetMonthlyInvoiceById(int id)
        {
            var response = await _monthlyInvoiceService.GetById(id, "Apartments");
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse>> AddMonthlyInvoice([FromBody] MonthlyInvoiceRequest request)
        {
            var response = await _monthlyInvoiceService.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse>> UpdateMonthlyInvoice(int id, [FromBody] MonthlyInvoiceRequest request)
        {
            var response = await _monthlyInvoiceService.Update(id, request);
            return response;
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse>> DeleteMonthlyInvoice(int id)
        {
            var response = await _monthlyInvoiceService.Delete(id);
            return response;
        }
    
    }
}

