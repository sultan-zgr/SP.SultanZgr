using Microsoft.AspNetCore.Identity;
using SP.Business.Abstract;
using SP.Business.Concrete;
using SP.Business.GenericService;
using SP.Business.Token;
using SP.Data;
using SP.Data.UnitOfWork;
using SP.Entity;
using SP.Entity.Models;

namespace SP.API
{
    public static class DependencyInjection
    {

        public static void AddDependencyInjection(this IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));

            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IUserLogService, UserLogService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IMonthlyInvoiceService, MonthlyInvoiceService>();




        }
    }
}
