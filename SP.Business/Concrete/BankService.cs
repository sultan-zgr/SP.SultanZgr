using AutoMapper;
using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.PaymentService;
using SP.Data;
using SP.Data.UnitOfWork;
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
    public class BankService : PaymentService<Bank, BankRequest, BankResponse>, IBankService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BankService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Bank> GetBankByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateBankInfo(BankRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
