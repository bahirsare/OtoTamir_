﻿using Microsoft.EntityFrameworkCore;
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

        public  int Delete(int id)
        {
            var entity =  _context.Set<T>().Find(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
            return _context.SaveChanges();
        }
    }
}
