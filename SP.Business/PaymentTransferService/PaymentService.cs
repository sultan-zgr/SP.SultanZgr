using AutoMapper;
using Patika.Entity.Models;
using Serilog;
using SP.Base.BaseResponse;
using SP.Business.GenericService;
using SP.Data;
using SP.Schema.Request;
using SP.Schema.Response;
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

        public Task<ApiResponse> InvoicePayment(string userId, decimal amount)
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


//public async Task<PaymentResponse> PayInvoiceAsync(PaymentRequest paymentRequest)
//{
//    // Ödeme isteği geçerli mi diye kontrol edin
//    if (paymentRequest == null || paymentRequest.InvoiceAmount <= 0)
//    {
//        return new PaymentResponse { Message = "Invalid payment request" };
//    }

//    // Kullanıcının cüzdan bilgisini alın
//    ApiResponse<UserResponse> userResponse = await _userService.GetById(paymentRequest.UserId);
//    if (!userResponse.Success || userResponse.Response == null)
//    {
//        return new PaymentResponse { Message = "User not found" };
//    }

//    // Kullanıcının cüzdan tutarı yeterli mi diye kontrol edin
//    if (userResponse.Response.Wallet < paymentRequest.InvoiceAmount)
//    {
//        return new PaymentResponse { Message = "Insufficient funds in the wallet" };
//    }

//    // Ödeme işlemini yapın ve cüzdanı güncelleyin
//    try
//    {
//        // Ödeme işlemini yapmak için bir metot çağırabilirsiniz
//        ApiResponse<PaymentResponse> paymentResponse = await WithdrawFromWallet(paymentRequest.UserId, paymentRequest.InvoiceAmount);

//        if (!paymentResponse.Success)
//        {
//            return new PaymentResponse { Message = "Payment failed. " + paymentResponse.Message };
//        }

//        // Ödeme başarılıysa, cüzdan tutarını güncelleyin
//        userResponse.Response.Wallet -= paymentRequest.InvoiceAmount;

//        // Cüzdan bilgisini veritabanına kaydedin
//        // Burada, UserResponse nesnesini uygun şekilde kullanılan model sınıfıyla değiştirerek ve gerekirse hizmetlerinizle uyumlu hale getirerek kaydetme işlemi yapmanız gerekir.

//        return new PaymentResponse { Message = "Payment processed successfully" };
//    }
//    catch (Exception ex)
//    {
//        return new PaymentResponse { Message = "An error occurred while processing the payment." };
//    }
//}

//private async Task<ApiResponse<PaymentResponse>> WithdrawFromWallet(int userId, decimal amount)
//{
//    // Bu metot, kullanıcının cüzdanından belirli bir tutarı çekmek için kullanılır.
//    // İşlemin başarılı olup olmadığını ve diğer bilgileri ApiResponse içinde döndürebilirsiniz.

//    // Örnek bir ApiResponse<TransferResponse> oluşturun (işlem başarılıysa)
//    var response = new ApiResponse<PaymentResponse>(true)
//    {
//        Response = new PaymentResponse
//        {
//            Message = "1234567890" // Örnek bir referans numarası
//        }
//    };

//    // Burada gerçek çekme işlemini yapmanız gerekecek, yani kullanıcının cüzdanından amount tutarında düşüm yapmanız gerekecek.

//    return response;
//}
