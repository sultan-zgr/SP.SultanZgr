using AutoMapper;
using SP.Schema.Request.UserLog;
using SP.Schema.Request;
using SP.Schema.Response.UserLog;
using SP.Schema.Response;
using SP.Entity;
using SP.Entity.Models;

namespace SP.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Admin, AdminRequest>();
        CreateMap<Admin, AdminResponse>();

        CreateMap<UserLogRequest, UserLog>();
        CreateMap<UserLog, UserLogResponse>();

        CreateMap<ApartmentRequest, Apartment>();  
        CreateMap<Apartment, ApartmentResponse>();

        CreateMap<MonthlyInvoiceRequest, MonthlyInvoice>();
        CreateMap<MonthlyInvoiceResponse, MonthlyInvoice>();
    }

}

