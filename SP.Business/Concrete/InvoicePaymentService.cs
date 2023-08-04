using AutoMapper;
using Patika.Entity.Models;
using Serilog;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Data;
using SP.Schema.Request;
using SP.Schema.Response;
using SP.Base;
using SP.Data.UnitOfWork;
using SP.Entity;

namespace SP.Business.Concrete
{
    public class InvoicePaymentService : IInvoicePaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccountService _userAccountService;
        private readonly IMonthlyInvoiceService _monthlyInvoiceService;

        public InvoicePaymentService(IMapper mapper, IUnitOfWork unitOfWork, IUserAccountService userAccountService, IMonthlyInvoiceService monthlyInvoiceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userAccountService = userAccountService;
            _monthlyInvoiceService = monthlyInvoiceService;
        }



        public async Task<ApiResponse<List<PaymentResponse>>> GetAllAsync()
        {
            try
            {
                var entityList = await _unitOfWork.DynamicRepo<Payment>().GetAllAsync();
                var mapped = _mapper.Map<List<Payment>, List<PaymentResponse>>(entityList);
                return new ApiResponse<List<PaymentResponse>>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PaymentService.GetAllAsync");
                return new ApiResponse<List<PaymentResponse>>(ex.Message);
            }
        }

        public Task<ApiResponse<PaymentResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<TransferReponse>> PayAsync(CashRequest request)
        {
            // Your existing code to check the request and retrieve the user account
            if (request == null)
            {
                return new ApiResponse<TransferReponse>("Invalid Request");
            }

            ApiResponse<UserAccountResponse> accountResponse = await _userAccountService.GetById(request.UserId);
            UserAccountResponse userAccount = accountResponse.Response;

            // Ödeme işlemi için gerekli kontroller
            if (userAccount == null)
            {
                return new ApiResponse<TransferReponse>("Invalid User Account");
            }

            // Check if the user has any monthly invoices
            
            ApiResponse<MonthlyInvoiceResponse> monthlyInvoiceResponse = await _monthlyInvoiceService.GetById(request.UserId);
            MonthlyInvoiceResponse userMonthlyInvoice = monthlyInvoiceResponse.Response;


            // Calculate the total amount due for the monthly invoices
            if (userMonthlyInvoice == null)
            {
                return new ApiResponse<TransferReponse>("User does not have any monthly invoices");
            }

            // Ödeme tutarını kontrol et
            decimal totalMonthlyInvoiceAmount = userMonthlyInvoice.DuesAmount;

            // Calculate the total payment amount (requested payment + total monthly invoice amount)
            decimal paymentAmount = request.Amount; // Fatura tutarınızı buraya ekleyin ya da başka bir şekilde temin edin.
            decimal totalPaymentAmount = paymentAmount - totalMonthlyInvoiceAmount;

            // Ödeme tutarını kontrol et
            if (userAccount.Balance < totalPaymentAmount)
            {
                return new ApiResponse<TransferReponse>("Insufficient Balance");
            }
            
            // Ödeme işlemini gerçekleştir
            Payment payment = new Payment
            {
                UserId = userAccount.UserId,
                MonthlyInvoiceId = userMonthlyInvoice.MonthlyInvoiceId, // Burada Payments tablosundaki uygun MonthlyInvoiceId'yi atamanız gerekiyor.
                PaymentDate = DateTime.Now,
                InvoiceAmount = paymentAmount,
                Balance = userAccount.Balance - paymentAmount,
                IsSuccessful = true,
                Message = "Ödeme başarıyla gerçekleştirildi.",
                NewBalance = userAccount.Balance - paymentAmount,


            };


            // Payment nesnesini veritabanına ekleyin
            await _unitOfWork.DynamicRepo<Payment>().InsertAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            // Deduct the total monthly invoice amount from the user's balance
            userAccount.Balance -= totalMonthlyInvoiceAmount;

            // Kullanıcının güncellenmiş cüzdan bakiyesini _userAccountService üzerinden güncelleyin
            UserAccountRequest userAccountRequest = new UserAccountRequest
            {
                UserId = userAccount.UserId,
                Balance = userAccount.Balance,

                // Diğer güncellenecek alanları da ekleyin
            };

            // Kullanıcının güncellenmiş cüzdan bakiyesini _userAccountService üzerinden güncelleyin
            await _userAccountService.Update(userAccount.UserId, userAccountRequest);

            // Sonuçları hazırlayın ve döndürün
            PaymentResponse paymentResponse = new PaymentResponse
            {
                PaymentId = payment.Id,
                IsSuccessful = true,
                Message = "Payment Successful",
                NewBalance = userAccount.Balance,
                PaymentDate = DateTime.Now
            };

            return new ApiResponse<TransferReponse>("Payment Successful");
        }

    }

}
      
    




