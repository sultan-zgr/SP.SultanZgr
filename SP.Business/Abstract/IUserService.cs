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
    Task<bool> CheckUserExists(int userId); //Bu şekilde bir kullanıcımız var mı kontrol ediyoruz
 
}
