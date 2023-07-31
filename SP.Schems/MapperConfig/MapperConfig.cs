using AutoMapper;
using SP.Schema.Request.UserLog;
using SP.Schema.Request;
using SP.Schema.Response.UserLog;
using SP.Schema.Response;
using SP.Entity;

namespace SP.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserLogRequest, UserLog>();
        CreateMap<UserLog, UserLogResponse>();

        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
