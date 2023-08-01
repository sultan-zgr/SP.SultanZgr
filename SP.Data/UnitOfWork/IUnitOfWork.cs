using SP.Data.GenericRepo;
using SP.Entity;

namespace SP.Data;

public interface IUnitOfWork
{

    Task SaveChangesAsync();
    IGenericRepository<T> DynamicRepo<T>() where T :  class;

}
