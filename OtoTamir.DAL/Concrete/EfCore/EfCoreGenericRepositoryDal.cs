using Microsoft.EntityFrameworkCore;
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

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            var entities = _context.Set<T>().AsQueryable();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        }

        public T GetOne(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual int Create(T entity)
        {
            
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int Update()
        {
            return _context.SaveChanges();
        }

        public  int Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
            return _context.SaveChanges();
        }
    }
}

