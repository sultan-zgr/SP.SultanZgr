

using SP.Data;
using SP.Data.GenericRepo;

namespace SP.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SPDbContext _dbContext;

        public UnitOfWork(SPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<T> DynamicRepo<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
