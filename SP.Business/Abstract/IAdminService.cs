using SP.Business.GenericService;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.Business.Abstract
{
    public interface IAdminService : IGenericService<Admin, AdminRequest, AdminResponse>
    {
    }
}
