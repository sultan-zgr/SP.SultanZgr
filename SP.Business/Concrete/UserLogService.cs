using AutoMapper;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Concrete
{
    public class UserLogService : GenericService<UserLog, UserLogRequest, UserLogResponse>, IUserLogService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserLogService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

    
    }
}
