using AutoMapper;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Entity;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.Business.Concrete;

public class UserService : GenericService<User, UserRequest, UserResponse>, IUserService
{

    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
}
