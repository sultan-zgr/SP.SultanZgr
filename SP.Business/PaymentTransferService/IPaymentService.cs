using SP.Base.BaseResponse;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.PaymentService
{
    public interface IPaymentService<T,TRequest, TResponse>
    {
        // Yeni bir ödeme işlemi oluşturma
        Task<ApiResponse<List<TResponse>>> GetAllPayment(params string[] includes);
        Task<ApiResponse<TResponse>> GetByPaymentId(int id, params string[] includes);
        Task<ApiResponse> Insert(TRequest request);
        Task<ApiResponse> Update(int Id, TRequest request);
        Task<ApiResponse> Delete(int Id);
        // Ödeme işlemi
        Task<ApiResponse> MakePayment(string userId, decimal amount);

    }

}
