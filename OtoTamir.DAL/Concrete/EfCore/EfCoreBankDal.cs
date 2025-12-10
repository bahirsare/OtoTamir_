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
        public override async Task<int> CreateAsync(Bank bank)
        {
            bank.CreatedDate = DateTime.Now;
            bank.ModifiedDate = DateTime.Now;
            bank.BankName= bank.BankName.ToUpper();
            return await base.CreateAsync(bank);
        }
        public async Task<Bank> GetOneAsync(
        int id,
        string mechanicId)
        {
            var query = _context.Banks.Where(b => b.Id == id && b.Treasury.MechanicId ==mechanicId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<Bank>> GetAllAsync(
        string mechanicId,
        
        Expression<Func<Bank, bool>> filter = null
        )
        {
            var query = _context.Banks
                .Where(b => b.Treasury.MechanicId == mechanicId);



            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

    }
}
