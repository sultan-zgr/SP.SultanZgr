using Patika.Entity.Models;
using SP.Business.PaymentService;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Abstract
{
    public interface IPaymentTransferService : IPaymentService<Payment, PaymentRequest, PaymentResponse> 
    {

    
    }
}
