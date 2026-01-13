using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Utilities;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreGenericRepositoryDal<T, TContext> where T : class where TContext : DbContext
    {
        private readonly TContext _context;

        public EfCoreGenericRepositoryDal(TContext context)
        {
            _context = context;
        }


        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().AnyAsync(filter);
        }


        public virtual async Task<int> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);

                return await _context.SaveChangesAsync();
            }

            return 0;
        }
        public async Task<PagedResult<T>> GetPagedAsync(
    Expression<Func<T, bool>> filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    int page = 1,
    int pageSize = 10,
    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();


            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }


            if (filter != null)
            {
                query = query.Where(filter);
            }

            var rowCount = await query.CountAsync();

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {

            }

            var pageCount = (double)rowCount / pageSize;
            int pageCountInt = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            var results = await query.Skip(skip).Take(pageSize).ToListAsync();


            return new PagedResult<T>
            {
                Results = results,
                CurrentPage = page,
                PageCount = pageCountInt,
                PageSize = pageSize,
                RowCount = rowCount
            };
        }
    }
}

