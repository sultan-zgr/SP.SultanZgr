using AutoMapper;
using Patika.Entity.Models;
using SP.Business.Abstract;
using SP.Business.PaymentService;
using SP.Data;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Concrete
{
    public class PaymentTransferService : PaymentService<Payment, PaymentRequest, PaymentResponse>, IPaymentTransferService
    {
        public PaymentTransferService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }
    }
}
