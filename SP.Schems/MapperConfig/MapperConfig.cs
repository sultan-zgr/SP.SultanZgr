using AutoMapper;
using SP.Schema.Request;
using SP.Schema.Response;
using SP.Entity;
using SP.Entity.Models;

namespace SP.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserLog, UserLogRequest>();
        CreateMap<UserLog, UserLogResponse>();

        CreateMap<User, UserRequest>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();

        CreateMap<ApartmentRequest, Apartment>();
        CreateMap<Apartment, ApartmentResponse>();

        CreateMap<MonthlyInvoice, MonthlyInvoiceRequest>();
        CreateMap<MonthlyInvoice, MonthlyInvoiceResponse>()
    .ForMember(dest => dest.Apartment, opt => opt.MapFrom(src => src.Apartment));

    }

}

