using SP.Business.GenericService;
using SP.Entity;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.Business.Abstract;

public interface IUserService : IGenericService<User, UserRequest, UserResponse>
{

}
