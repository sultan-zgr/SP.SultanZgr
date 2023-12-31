﻿using AutoMapper;
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
        private readonly IApartmentService _apartmentService;


        public InvoicePaymentService(IMapper mapper, IUnitOfWork unitOfWork, IUserAccountService userAccountService, IMonthlyInvoiceService monthlyInvoiceService, IApartmentService apartmentService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userAccountService = userAccountService;
            _monthlyInvoiceService = monthlyInvoiceService;
            _apartmentService = apartmentService;
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

        public Task<ApiResponse<bool>> IsPaidAsync(CashRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<TransferReponse>> PayAsync(CashRequest request)
        {
            if (request == null)
                return new ApiResponse<TransferReponse>("Invalid Request");

            if (request.CreditCardNumber == null || request.CreditCardNumber.Length != 16)
                return new ApiResponse<TransferReponse>("Invalid Credit Card Number");

            // Validate CVV
            if (request.CVV == null || request.CVV.Length != 3)
                return new ApiResponse<TransferReponse>("Invalid CVV");

            // Validate expiration date
            if (request.ExpirationDate == null || request.ExpirationDate.Length != 5)
                return new ApiResponse<TransferReponse>("Invalid Expiration Date");


            User userAccount = await _unitOfWork.DynamicRepo<User>().GetByIdAsync(request.UserId);
            if (userAccount == null)
                return new ApiResponse<TransferReponse>("Invalid User Account");


            MonthlyInvoice monthlyInvoice = await _unitOfWork.DynamicRepo<MonthlyInvoice>().GetByIdAsync(request.MonthlyInvoiceId);
            if (monthlyInvoice.PaymentStatus == true) // Ödeme zaten yapıldıysa
                return new ApiResponse<TransferReponse>("Invoice has already been paid.");

            if (monthlyInvoice == null)
                return new ApiResponse<TransferReponse>("User does not have any monthly invoices");

            decimal invoiceAmount = monthlyInvoice.InvoiceAmount;
            Payment payment = new Payment
            {
                UserId = userAccount.UserId,
                MonthlyInvoiceId = monthlyInvoice.MonthlyInvoiceId,
                PaymentDate = DateTime.Now,
                InvoiceAmount = invoiceAmount,
                Balance = userAccount.Balance - invoiceAmount,
                IsSuccessful = true,
                Message = "Payment successfully completed",
                NewBalance = userAccount.Balance - invoiceAmount
            };
            userAccount.Balance -= invoiceAmount;

            await _unitOfWork.DynamicRepo<Payment>().InsertAsync(payment);
            await _unitOfWork.DynamicRepo<User>().UpdateAsync(userAccount);
            monthlyInvoice.PaymentStatus = true;
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






