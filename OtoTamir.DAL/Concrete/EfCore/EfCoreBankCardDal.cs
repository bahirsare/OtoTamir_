using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreBankCardDal : EfCoreGenericRepositoryDal<BankCard, DbContext>, IBankCardDal
    {
        private readonly DataContext _context;

        public EfCoreBankCardDal(DataContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<int> CreateAsync(BankCard bankCard)
        {
            bankCard.CreatedDate = DateTime.Now;
            bankCard.ModifiedDate = DateTime.Now;
            return await base.CreateAsync(bankCard);
        }
        public async Task<BankCard> GetOneAsync(
        int id,
        string mechanicId)
        {
            var query = _context.BankCards.Where(bc => bc.Id == id && bc.Bank.Treasury.MechanicId == mechanicId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<BankCard>> GetAllAsync(
        string mechanicId,
        int treasuryId,
        Expression<Func<BankCard, bool>> filter = null
        )
        {
            var query = _context.BankCards
                .Where(bc => bc.Bank.TreasuryId == treasuryId && bc.Bank.Treasury.MechanicId == mechanicId);



            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
        
    }
}
