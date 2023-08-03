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
    public class InvoicePaymentController : ControllerBase
    {
        private readonly IInvoicePaymentService _service;
        private readonly IBankService _bankService;

        public InvoicePaymentController(IBankService bankService, IInvoicePaymentService paymentTransferService)
        {
            _service = paymentTransferService;
            _bankService = bankService;
        }
        [HttpGet]
        public async Task<ApiResponse<List<PaymentResponse>>> GetAllPayments()
        {
            var response = await _service.GetAllPayment();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<PaymentResponse>> GetPaymentsById(int id)
        {
            var response = await _service.GetByPaymentId(id);
            return response;
        }

        [HttpPost]
        public async Task<ApiResponse> InsertPayment(PaymentRequest request)
        {

            // PaymentService üzerinden ödeme ekleme işlemini gerçekleştirin.
            var response = await _service.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdatePayment(int id, PaymentRequest request)
        {
            // PaymentService üzerinden ödeme güncelleme işlemini gerçekleştirin.
            var response = await _service.Update(id, request);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeletePayment(int id)
        {
            // PaymentService üzerinden ödeme silme işlemini gerçekleştirin.
            var response = await _service.Delete(id);
            return response;
        }
        [HttpPost("pay")]
        public async Task<ApiResponse> PayInvoice(int id)
        {
            // Ödeme işlemini gerçekleştirin.
        throw new NotImplementedException();
        }



    }
}

