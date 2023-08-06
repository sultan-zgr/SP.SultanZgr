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

        CreateMap<ApartmentRequest, Apartment>();
        CreateMap<Apartment, ApartmentResponse>();

        CreateMap<MonthlyInvoice, MonthlyInvoiceRequest>().ReverseMap();
        CreateMap<MonthlyInvoice, MonthlyInvoiceResponse>().ReverseMap()
    .ForMember(dest => dest.Apartment, opt => opt.MapFrom(src => src.Apartment));

        CreateMap<PaymentRequest, Payment>().ReverseMap();
        CreateMap<PaymentResponse, Payment>().ReverseMap(); 

        CreateMap<Bank, BankRequest>().ReverseMap(); 
        CreateMap<Bank, BankResponse>().ReverseMap(); 

        CreateMap<Messages,MessagesRequest>().ReverseMap();
        CreateMap<Messages,MessagesResponse>().ReverseMap();
    }

}

