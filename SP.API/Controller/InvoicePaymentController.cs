using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Data.UnitOfWork;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;
using System.Data;

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
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<PaymentResponse>>> GetAllPayments()
        {
            var response = _service.GetAllAsync();
            return await response;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<PaymentResponse>> GetPaymentsById(int id)
        {
            var response = _service.GetByIdAsync(id);
            return await response;
        }
        [HttpPost("pay")]
        [Authorize(Roles = "admin,user")]
        public async Task<ApiResponse<TransferReponse>> PayInvoice([FromBody] CashRequest request)
        {

            var response = await _service.PayAsync(request);
            return response;
        }




    }
}

