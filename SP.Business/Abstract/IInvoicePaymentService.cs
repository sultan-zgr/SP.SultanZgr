using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.GenericService;
using SP.Entity;
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
    public interface IInvoicePaymentService 
    {
        Task<ApiResponse<List<PaymentResponse>>> GetAllAsync();
        Task<ApiResponse<PaymentResponse>> GetByIdAsync(int id);
        Task<ApiResponse<TransferReponse>> PayAsync(CashRequest request);
    }
}
