using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Data.UnitOfWork;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.API.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class InvoicePaymentController : ControllerBase
    {
        private readonly IInvoicePaymentService _service;

        public InvoicePaymentController( IInvoicePaymentService paymentTransferService)
        {
            _service = paymentTransferService;
          
        }
        [HttpGet]
        public async Task<ApiResponse<List<PaymentResponse>>> GetAllPayments()
        {
            var response = _service.GetAllAsync();
            return await response;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<PaymentResponse>> GetPaymentsById(int id)
        {
            var response = _service.GetByIdAsync(id);
            return await response;
        }
        [HttpPost("pay")]
        public async Task<ApiResponse<TransferReponse>> PayInvoice([FromBody] CashRequest request)
        {

            var response = await _service.PayAsync(request);
            return response;
        }




    }
}

