using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreBankDal : EfCoreGenericRepositoryDal<Bank, DataContext>,IBankDal
    {

        private readonly DataContext _context;

        public EfCoreBankDal(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Bank> GetOneAsync(
        int id,
        string mechanicId)
        {
            var query = _context.Banks.Where(b => b.Id == id && b.MechanicId == mechanicId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<Bank>> GetAllAsync(
        string mechanicId,
        int clientId,
        Expression<Func<Bank, bool>> filter = null
        )
        {
            var query = _context.Banks
                .Where(b => b.MechanicId == mechanicId);



            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

    }
}
