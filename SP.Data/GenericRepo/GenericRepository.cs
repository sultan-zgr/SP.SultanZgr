using Microsoft.EntityFrameworkCore;
using SP.Entity;
using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SP.Data.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SPDbContext _dbContext;

        public GenericRepository(SPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public Task<List<T>> GetAllWithIncludeAsync(params string[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
     
            return query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public Task<T> GetByIdWithIncludeAsync(int id, params string[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = includes.Aggregate(query, (current, inc) => current.Include(inc)); 
            return query.FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(T t)
        {
            _dbContext.Set<T>().Update(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<T> InsertAsync(T t)
        {
            // t. = "sultanadmin@gmail.com";
            await _dbContext.Set<T>().AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }
        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsQueryable();
        }

      

      
    }
}