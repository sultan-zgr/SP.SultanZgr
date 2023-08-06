using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SP.Data.GenericRepo
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithIncludeAsync(params string[] includes); //İlgili nesneyi ve gerekise ilişkilili entityleri de birlikte almak için includes da ekliyorum
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithIncludeAsync(int id, params string[] includes);  //İlgili nesneyi ve gerekise ilişkilili entityleri de birlikte almak için includes da ekliyorum
        Task<T> InsertAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
        IEnumerable<T> Where(Expression<Func<T, bool>> expression);
  
    }

}

