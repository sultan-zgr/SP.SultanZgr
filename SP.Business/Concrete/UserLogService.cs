
using AutoMapper;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Entity;
using SP.Schema.Request.UserLog;
using SP.Schema.Response.UserLog;

namespace SP.Business.Concrete;

public class UserLogService : GenericService<UserLog, UserLogRequest, UserLogResponse>, IUserLogService
{

    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public UserLogService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public ApiResponse<List<UserLogResponse>> GetByUserSession(string username)
    {
        var list = unitOfWork.DynamicRepo<User>().Where(x => x.UserName == username).ToList();  // !!!

        var mapped = mapper.Map<List<UserLogResponse>>(list);
        return new ApiResponse<List<UserLogResponse>>(mapped);
    }
}
