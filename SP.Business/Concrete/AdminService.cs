using AutoMapper;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.Business.Concrete
{
    public class AdminService : GenericService<Admin, AdminRequest, AdminResponse>, IAdminService
    {
        public AdminService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }
    }
}
