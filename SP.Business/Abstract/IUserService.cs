using SP.Base.BaseResponse;
using SP.Business.GenericService;
using SP.Entity;
using SP.Entity.Models;
using SP.Schema;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.Business.Abstract;

public interface IUserService : IGenericService<User, UserRequest, UserResponse>
{
    Task<ApiResponse<List<User>>> GetUsersWithPendingPayments();  //Faturasını ödemeyen kullanıcılar
}
