using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.PaymentService;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Abstract
{
    public interface IBankService : IPaymentService<Bank, BankRequest, BankResponse> 
    {
        Task<Bank> GetBankByUserId(string userId);
        Task<ApiResponse> UpdateBankInfo(BankRequest request); 

    }
}
