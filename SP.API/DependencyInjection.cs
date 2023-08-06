
using SP.API.Controller;
using SP.API.RabbitMq.Producer;
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



            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IMonthlyInvoiceService, MonthlyInvoiceService>();

            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IInvoicePaymentService, InvoicePaymentService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLogService, UserLogService>();

         

            services.AddScoped<IInvoicePaymentService, InvoicePaymentService>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();
       

           //ervices.AddScoped<IInvoicePaymentService, InvoicePaymentService>();


        }
    }
}
