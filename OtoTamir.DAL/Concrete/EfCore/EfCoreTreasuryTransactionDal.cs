using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreTreasuryTransactionDal : EfCoreGenericRepositoryDal<TreasuryTransaction, DbContext>, ITreasuryTransactionDal
    {
        private readonly DataContext _context;

        public EfCoreTreasuryTransactionDal(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TreasuryTransaction> GetOneAsync(
        int id,
        string mechanicId)
        {
            var query = _context.Transactions.Where(b => b.Id == id && b.Treasury.MechanicId == mechanicId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<TreasuryTransaction>> GetAllAsync(
        string mechanicId,
        int treasuryId,
        Expression<Func<TreasuryTransaction, bool>> filter = null
        )
        {
            var query = _context.Transactions
                .Where(b => b.TreasuryId == treasuryId && b.Treasury.MechanicId == mechanicId);



            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
        public override async Task<int> CreateAsync(TreasuryTransaction transaction)
        {
            if (transaction.TransactionType == TransactionType.Outgoing && transaction.Amount > 0)
                transaction.Amount *= -1;
            return await base.CreateAsync(transaction);
        }
        public async Task<decimal> GetTotalBalanceAsync(int treasuryId)
        {
            return await _context.Transactions
                .Where(t => t.TreasuryId == treasuryId)
                .SumAsync(t => t.Amount);
        }

        public async Task<List<TreasuryTransaction>> GetByPaymentSourceAsync(int treasuryId, string mechanicId, PaymentSource source)
        {
            return await _context.Transactions
          .Where(t => t.TreasuryId == treasuryId &&t.Treasury.MechanicId==mechanicId && t.PaymentSource == source)
          .ToListAsync();
        }

        public async Task<List<TreasuryTransaction>> GetByDateRangeAsync(int treasuryId, string mechanicId, DateTime start, DateTime end)
        {
            return await _context.Transactions
            .Where(t => t.TreasuryId == treasuryId && t.Treasury.MechanicId == mechanicId && t.TransactionDate >= start && t.TransactionDate <= end)
            .ToListAsync();
        }
    }
}