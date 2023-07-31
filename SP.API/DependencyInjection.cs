using SP.Business.Abstract;
using SP.Business.Concrete;
using SP.Business.GenericService;
using SP.Data;
using SP.Data.UnitOfWork;

namespace SP.API
{
    public static class DependencyInjection
    {

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));

            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IUserLogService, UserLogService>();
            services.AddScoped<IUserService, UserService>();





        }
    }
}
