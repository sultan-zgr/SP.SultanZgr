using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.PaymentService;
using SP.Data.PaymentRepo;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTransferController : ControllerBase
    {
        private readonly IPaymentTransferService _service;
        private readonly IBankService _bankService;

        public PaymentTransferController(IBankService bankService , IPaymentTransferService paymentTransferService)
        {
            _service = paymentTransferService;
            _bankService = bankService;
        }
        [HttpGet]
        public async Task<ApiResponse<List<PaymentResponse>>> GetAllPayments()
        {
            var response = await _service.GetAllPayment("User");
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<PaymentResponse>> GetPaymentsById(int id)
        {
            var response = await _service.GetByPaymentId(id);
            return response;
        }
      


    }
}