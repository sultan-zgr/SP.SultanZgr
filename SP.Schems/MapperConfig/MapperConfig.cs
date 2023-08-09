using AutoMapper;
using SP.Schema.Request;
using SP.Schema.Response;
using SP.Entity;
using SP.Entity.Models;
using Patika.Entity.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace SP.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserLog, UserLogRequest>();
        CreateMap<UserLog, UserLogResponse>();

        CreateMap<User, UserRequest>().ReverseMap();
        CreateMap<User, UserAccountResponse>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();

        CreateMap<ApartmentRequest, Apartment>().ReverseMap();
        CreateMap<Apartment, ApartmentResponse>().ReverseMap();

        CreateMap<MonthlyInvoice, MonthlyInvoiceRequest>().ReverseMap();
        CreateMap<MonthlyInvoice, MonthlyInvoiceResponse>().ReverseMap();
  

        CreateMap<PaymentRequest, Payment>().ReverseMap();
        CreateMap<PaymentResponse, Payment>().ReverseMap(); 

        CreateMap<Bank, BankRequest>().ReverseMap(); 
        CreateMap<Bank, BankResponse>().ReverseMap(); 

        CreateMap<Messages,MessagesRequest>().ReverseMap();
        CreateMap<Messages,MessagesResponse>().ReverseMap();

    }

}

