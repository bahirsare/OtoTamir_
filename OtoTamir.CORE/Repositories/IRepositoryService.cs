using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Repositories
{
    public interface IRepositoryService<T> where T : class
    {
        Task<List<T>> GetAllAsync();
       Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        //Task<T> GetOneAsync(int id,string mechanicId);
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync();
        Task<int> DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    }
}
