using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Base;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Data.UnitOfWork;
using SP.Entity;
using SP.Entity.Models;
using SP.Schema;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.Business.Concrete;

public class UserService : GenericService<User, UserRequest, UserResponse>, IUserService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CheckUserExists(int userId)
    {
        try
        {
            var user = await _unitOfWork.DynamicRepo<User>().FindAsync(u => u.UserId == userId);
            return user != null;
        }
        catch (Exception ex)
        { 
            return false;
        }
    }
  



}


