using AutoMapper;
using Patika.Entity.Models;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.PaymentService;
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
    public class InvoicePaymentService : PaymentService<Payment, PaymentRequest, PaymentResponse>, IInvoicePaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
 
        public InvoicePaymentService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            
        }

        public async Task<PaymentResponse> PayInvoiceAsync(PaymentRequest paymentRequest)
        {
            // Ödeme isteği geçerli mi diye kontrol edin
            if (paymentRequest == null || paymentRequest.InvoiceAmount <= 0)
            {
                return new PaymentResponse { Message = "Invalid payment request" };
            }

            // Kullanıcının cüzdan bilgisini alın
            ApiResponse<UserResponse> userResponse = await _userService.GetById(paymentRequest.UserId);
            if (!userResponse.Success || userResponse.Response == null)
            {
                return new PaymentResponse { Message = "User not found" };
            }

            // Kullanıcının cüzdan tutarı yeterli mi diye kontrol edin
            if (userResponse.Response.Wallet < paymentRequest.InvoiceAmount)
            {
                return new PaymentResponse { Message = "Insufficient funds in the wallet" };
            }

            // Ödeme işlemini yapın ve cüzdanı güncelleyin
            try
            {
                // Ödeme işlemini yapmak için bir metot çağırabilirsiniz
                ApiResponse<PaymentResponse> paymentResponse = await WithdrawFromWallet(paymentRequest.UserId, paymentRequest.InvoiceAmount);

                if (!paymentResponse.Success)
                {
                    return new PaymentResponse { Message = "Payment failed. " + paymentResponse.Message };
                }

                // Ödeme başarılıysa, cüzdan tutarını güncelleyin
                userResponse.Response.Wallet -= paymentRequest.InvoiceAmount;

                // Cüzdan bilgisini veritabanına kaydedin
                // Burada, UserResponse nesnesini uygun şekilde kullanılan model sınıfıyla değiştirerek ve gerekirse hizmetlerinizle uyumlu hale getirerek kaydetme işlemi yapmanız gerekir.

                return new PaymentResponse { Message = "Payment processed successfully" };
            }
            catch (Exception ex)
            {
                return new PaymentResponse { Message = "An error occurred while processing the payment." };
            }
        }

        private async Task<ApiResponse<PaymentResponse>> WithdrawFromWallet(int userId, decimal amount)
        {
            // Öncelikle, kullanıcının cüzdan bilgisini alın
            ApiResponse<UserResponse> userResponse = await _userService.GetById(userId);

            if (!userResponse.Success || userResponse.Response == null)
            {
                // Kullanıcı bulunamazsa veya bir hata olursa, hata mesajını döndürün
                return new ApiResponse<PaymentResponse>("User not found");
            }

            // Kullanıcının cüzdan tutarı yeterli mi diye kontrol edin
            if (userResponse.Response.Wallet < amount)
            {
                // Cüzdan tutarı yetersizse, hata mesajını döndürün
                return new ApiResponse<PaymentResponse>("Insufficient funds in the wallet");
            }

            try
            {
                // Burada, kullanıcının cüzdanından çekilecek tutarı işlem yaparak çekin
                // Örneğin, veritabanında kullanıcının cüzdanını güncelleyerek çekimi gerçekleştirebilirsiniz.

                // Çekim işlemi başarılıysa, cüzdan tutarını güncelleyin
                userResponse.Response.Wallet -= amount;

                // Cüzdan bilgisini veritabanına kaydedin
                // Burada, UserResponse nesnesini uygun şekilde kullanılan model sınıfıyla değiştirerek ve gerekirse hizmetlerinizle uyumlu hale getirerek kaydetme işlemi yapmanız gerekir.

                // İşlem başarılı olduğunda, ApiResponse içinde TransferResponse nesnesini döndürün
                var response = new PaymentResponse
                {
                    
                    // Diğer istenen bilgileri de doldurabilirsiniz.
                };

                return new ApiResponse<PaymentResponse>(response);
            }
            catch (Exception ex)
            {
                // İşlem sırasında bir hata olursa, hata mesajını döndürün
                return new ApiResponse<PaymentResponse>("An error occurred while processing the payment.");
            }
        }



    }


}


