using SP.Base.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.GenericService
{
    public interface IGenericService<T, TRequest, TResponse>
    {
        Task<ApiResponse<List<TResponse>>> GetAll(params string[] includes);
        Task<ApiResponse<TResponse>> GetById(int id, params string[] includes);
        Task<ApiResponse> Insert(TRequest request);
        Task<ApiResponse> Update(int Id, TRequest request);
        Task<ApiResponse> Delete(int Id);
    }
}
