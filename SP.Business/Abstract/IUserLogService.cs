using SP.Base.BaseResponse;
using SP.Business.GenericService;
using SP.Entity;
using SP.Schema.Request.UserLog;
using SP.Schema.Response.UserLog;

namespace SP.Business.Abstract;

public interface IUserLogService : IGenericService<UserLog, UserLogRequest, UserLogResponse>
{
    ApiResponse<List<UserLogResponse>> GetByUserSession(string username);
}
