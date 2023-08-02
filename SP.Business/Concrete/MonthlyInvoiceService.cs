using AutoMapper;
using SP.Base.BaseResponse;
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
    public class MonthlyInvoiceService : GenericService<MonthlyInvoice, MonthlyInvoiceResponse, MonthlyInvoiceRequest>, IMonthlyInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MonthlyInvoiceService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<List<MonthlyInvoiceResponse>>> GetUserMonthlyInvoices(int apartmentId)
        {
            try
            {
                var invoicesTask = _unitOfWork.DynamicRepo<MonthlyInvoice>()
                    .GetAllWithIncludeAsync(includes: "Apartment"); // Include "User" relation to get user information in the response

                var invoices = await invoicesTask;
                var filteredInvoices = invoices.Where(m => m.Apartment.Id == apartmentId).ToList(); // Filter invoices by UserId

                var response = _mapper.Map<List<MonthlyInvoice>, List<MonthlyInvoiceResponse>>(filteredInvoices);
                return new ApiResponse<List<MonthlyInvoiceResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<MonthlyInvoiceResponse>>(ex.Message);
            }
        }

        public async Task<ApiResponse> Insert(MonthlyInvoiceRequest request)
        {
            try
            {
                var entity = _mapper.Map<MonthlyInvoiceRequest, MonthlyInvoice>(request);
                await _unitOfWork.DynamicRepo<MonthlyInvoice>().InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> Update(int Id, MonthlyInvoiceRequest request)
        {
            try
            {
                var invoice = await _unitOfWork.DynamicRepo<MonthlyInvoice>().GetByIdAsync(Id);
                if (invoice == null)
                {
                    return new ApiResponse("Fatura bulunamadı");
                }

                _mapper.Map(request, invoice);
                await _unitOfWork.DynamicRepo<MonthlyInvoice>().UpdateAsync(invoice);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        async Task<ApiResponse<List<MonthlyInvoiceResponse>>> IGenericService<MonthlyInvoice, MonthlyInvoiceRequest, MonthlyInvoiceResponse>.GetAll(params string[] includes)
        {
            try
            {
                var invoices = await _unitOfWork.DynamicRepo<MonthlyInvoice>()
                    .GetAllWithIncludeAsync(includes); // Include specified relationships if any

                var response = _mapper.Map<List<MonthlyInvoice>, List<MonthlyInvoiceResponse>>(invoices);
                return new ApiResponse<List<MonthlyInvoiceResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<MonthlyInvoiceResponse>>(ex.Message);
            }
        }

        async Task<ApiResponse<MonthlyInvoiceResponse>> IGenericService<MonthlyInvoice, MonthlyInvoiceRequest, MonthlyInvoiceResponse>.GetById(int id, params string[] includes)
        {
            try
            {
                var invoice = await _unitOfWork.DynamicRepo<MonthlyInvoice>()
                    .GetByIdWithIncludeAsync(id, includes); // Include specified relationships if any

                if (invoice == null)
                {
                    return new ApiResponse<MonthlyInvoiceResponse>("Fatura bulunamadı");
                }

                var response = _mapper.Map<MonthlyInvoice, MonthlyInvoiceResponse>(invoice);
                return new ApiResponse<MonthlyInvoiceResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<MonthlyInvoiceResponse>(ex.Message);
            }
        }
    }
}





