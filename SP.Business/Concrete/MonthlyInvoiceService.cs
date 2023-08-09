using AutoMapper;
using SP.Base.BaseResponse;
using SP.Base.Enums.Months;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Data.UnitOfWork;
using SP.Entity;
using SP.Entity.Models;
using SP.Schema;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Concrete
{
    public class MonthlyInvoiceService : GenericService<MonthlyInvoice, MonthlyInvoiceRequest, MonthlyInvoiceResponse>, IMonthlyInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public MonthlyInvoiceService(IMapper mapper, IUnitOfWork unitOfWork, IUserService userService) : base(mapper, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<ApiResponse<MonthlyInvoiceResponse>> AddMonthlyInvoiceByMonth(MonthlyInvoiceRequest monthlyInvoiceRequest, Months month)
        {

            var monthlyInvoice = _mapper.Map<MonthlyInvoice>(monthlyInvoiceRequest);

            var addedInvoice = await _unitOfWork.DynamicRepo<MonthlyInvoice>().InsertAsync(monthlyInvoice);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<MonthlyInvoiceResponse>(addedInvoice);

            return new ApiResponse<MonthlyInvoiceResponse>(response);
        }



        public async Task<ApiResponse<MonthlyInvoiceResponse>> UpdateMonthlyInvoiceByMonth(int invoiceId, MonthlyInvoiceRequest monthlyInvoiceRequest, Months month)
        {
            var monthlyInvoice = await _unitOfWork.DynamicRepo<MonthlyInvoice>().GetByIdAsync(invoiceId);
            if (monthlyInvoice == null)
            {
                return new ApiResponse<MonthlyInvoiceResponse>("Monthly invoice not found.");
            }

            monthlyInvoice = _mapper.Map(monthlyInvoiceRequest, monthlyInvoice);

            await _unitOfWork.DynamicRepo<MonthlyInvoice>().UpdateAsync(monthlyInvoice);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<MonthlyInvoiceResponse>(monthlyInvoice);

            return new ApiResponse<MonthlyInvoiceResponse>(response);
        }
        public async Task<ApiResponse<List<UserResponse>>> GetUsersWithUnpaidInvoicesAsync()
        {
            List<UserResponse> usersWithUnpaidInvoices = new List<UserResponse>();

            List<MonthlyInvoice> allInvoices = await _unitOfWork.DynamicRepo<MonthlyInvoice>().GetAllAsync();

            bool anyUnpaidInvoice = false;

            foreach (var invoice in allInvoices)
            {
                if (!invoice.PaymentStatus)
                {
                    anyUnpaidInvoice = true;
                    User user = await _unitOfWork.DynamicRepo<User>().GetByIdAsync(invoice.UserId);
                    if (user != null)
                    {
                        UserResponse userResponse = _mapper.Map<UserResponse>(user);
                        if (!usersWithUnpaidInvoices.Contains(userResponse))
                        {
                            usersWithUnpaidInvoices.Add(userResponse);
                        }
                    }
                }
            }

            if (!anyUnpaidInvoice)
            {
                return new ApiResponse<List<UserResponse>>("No unpaid invoice information available");
            }

            return new ApiResponse<List<UserResponse>>(usersWithUnpaidInvoices);
        }



    }


}



