using SP.Data.GenericRepo;
using SP.Entity;

namespace SP.Data;

public interface IUnitOfWork
{

    void SaveChanges();
    IGenericRepository<T> DynamicRepo<T>() where T :  class;

}
