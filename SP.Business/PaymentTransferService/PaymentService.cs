using AutoMapper;
using Serilog;
using SP.Base.BaseResponse;
using SP.Business.GenericService;
using SP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.PaymentService
{
    public class PaymentService<T, TRequest, TResponse> : IPaymentService<T, TRequest, TResponse> where T : class 
    {

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Delete(int Id)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetByIdAsync(Id);
                if (entity == null)
                {
                    return new ApiResponse("Record not found!");
                }
                await unitOfWork.DynamicRepo<T>().DeleteAsync(Id);
                await unitOfWork.SaveChangesAsync();                              //UOW İÇERİSİNDEN DÖNEN SAVECHANGESİ KULLANIYORUM
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "GenericService.Delete");
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<List<TResponse>>> GetAllPayment(params string[] includes)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetAllWithIncludeAsync();
                var mapped = mapper.Map<List<T>, List<TResponse>>(entity);
                return new ApiResponse<List<TResponse>>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenericService.GetAll");
                return new ApiResponse<List<TResponse>>(ex.Message);
            }
        }

        public async Task<ApiResponse<TResponse>> GetByPaymentId(int id, params string[] includes)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetByIdWithIncludeAsync(id, includes);
                var mapped = mapper.Map<T, TResponse>(entity);
                return new ApiResponse<TResponse>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenericService.GetById");
                return new ApiResponse<TResponse>(ex.Message);
            }
        }

        public async Task<ApiResponse> Insert(TRequest request)
        {
            try
            {
                // Öncelikle gelen request verisini T tipine dönüştürüyoruz.
                var entity = mapper.Map<TRequest, T>(request);

                // Ardından, T tipindeki veriyi repository üzerinden ekliyoruz.
                await unitOfWork.DynamicRepo<T>().InsertAsync(entity);
                await unitOfWork.SaveChangesAsync(); // Değişiklikleri kaydediyoruz.

                return new ApiResponse
                {
                    Success = true,
                    Message = "Kayıt başarıyla eklendi."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Kayıt eklenirken bir hata oluştu: " + ex.Message
                };
            }
        }

        public Task<ApiResponse> MakePayment(string userId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> ProcessPayment(int apartmentId, string creditCardNumber, DateTime expiryDate, string cvv, decimal amount)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> Update(int Id, TRequest request)
        {

            try
            {
                var exist = await unitOfWork.DynamicRepo<T>().GetByIdAsync(Id);
                if (exist == null)
                {
                    return new ApiResponse("Record not found!.");
                }

                var entity = mapper.Map<TRequest, T>(request);
                await unitOfWork.DynamicRepo<T>().UpdateAsync(entity);
                await unitOfWork.SaveChangesAsync();                                 //UOW İÇERİSİNDEN DÖNEN SAVECHANGESİ KULLANIYORUM
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                // Log.Error(ex, "GenericService.GetAll");
                return new ApiResponse(ex.Message);

            }
        }
    }
}
