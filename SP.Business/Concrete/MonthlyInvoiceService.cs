using AutoMapper;
using SP.Base.BaseResponse;
using SP.Base.Enums.Months;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Data.UnitOfWork;
using SP.Entity;
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
    public class MonthlyInvoiceService : GenericService<MonthlyInvoice, MonthlyInvoiceRequest, MonthlyInvoiceResponse>, IMonthlyInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MonthlyInvoiceService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
    }
}








