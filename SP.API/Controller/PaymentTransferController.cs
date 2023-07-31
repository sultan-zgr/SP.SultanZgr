using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.PaymentService;
using SP.Data.PaymentRepo;

namespace SP.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTransferController : ControllerBase
    {
        private readonly IPaymentTransferService _paymentTransferService;
        private readonly IBankService _bankService;

        public PaymentTransferController(IBankService bankService , IPaymentTransferService paymentTransferService)
        {
            _paymentTransferService = paymentTransferService;
            _bankService = bankService;
        }


    }
}