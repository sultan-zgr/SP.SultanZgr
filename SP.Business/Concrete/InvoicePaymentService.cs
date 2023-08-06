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
using SP.Entity.Models;

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
            if (request == null)
                return new ApiResponse<TransferReponse>("Invalid Request");

            if (request.CreditCardNumber == null || request.CreditCardNumber.Length != 19)
                return new ApiResponse<TransferReponse>("Invalid Credit Card Number");

            // Validate CVV
            if (request.CVV == null || request.CVV.Length == 3)
                return new ApiResponse<TransferReponse>("Invalid CVV");

            // Validate expiration date
            if (request.ExpirationDate == null || request.ExpirationDate.Length == 5)
                return new ApiResponse<TransferReponse>("Invalid Expiration Date");


            User userAccount = await _unitOfWork.DynamicRepo<User>().GetByIdAsync(request.UserId);

            if (userAccount == null)
                return new ApiResponse<TransferReponse>("Invalid User Account");


            MonthlyInvoice monthlyInvoice = await _unitOfWork.DynamicRepo<MonthlyInvoice>().GetByIdAsync(request.MonthlyInvoiceId);


            if (monthlyInvoice == null)
                return new ApiResponse<TransferReponse>("User does not have any monthly invoices");



            Payment payment = new Payment
            {
                UserId = userAccount.UserId,
                MonthlyInvoiceId = monthlyInvoice.MonthlyInvoiceId,
                PaymentDate = DateTime.Now,
                InvoiceAmount = request.Amount,
                Balance = userAccount.Balance - request.Amount,

                Message = "Ödeme başarıyla gerçekleştirildi.",
                NewBalance = userAccount.Balance - request.Amount
            };
            userAccount.Balance -= request.Amount;

            await _unitOfWork.DynamicRepo<Payment>().InsertAsync(payment);
            await _unitOfWork.DynamicRepo<User>().UpdateAsync(userAccount);
            await _unitOfWork.SaveChangesAsync();


            PaymentResponse paymentResponse = new PaymentResponse
            {
                PaymentId = payment.Id,

                Message = "Payment Successful",
                NewBalance = userAccount.Balance,
                PaymentDate = DateTime.Now
            };


            return new ApiResponse<TransferReponse>("Payment Successful");
        }

    }

}






