using AutoMapper;
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
    public class UserAccountService : GenericService<User, UserAccountRequest, UserAccountResponse>, IUserAccountService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}