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
       
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync();
        Task<int> DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    }
}
