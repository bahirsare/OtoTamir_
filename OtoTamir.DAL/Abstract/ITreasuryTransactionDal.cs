using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    public interface ITreasuryTransactionDal : IRepositoryService<TreasuryTransaction>
    {
        Task<decimal> GetTotalBalanceAsync(int treasuryId);
        Task<List<TreasuryTransaction>> GetAllAsync(
        string mechanicId,
        int treasuryId,
        Expression<Func<TreasuryTransaction, bool>> filter = null
        );
        Task<TreasuryTransaction> GetOneAsync(
        int id,
        string mechanicId);
        Task<List<TreasuryTransaction>> GetByPaymentSourceAsync(int treasuryId, string mechanicId, PaymentSource source);
        Task<List<TreasuryTransaction>> GetByDateRangeAsync(int treasuryId, string mechanicId, DateTime start, DateTime end);
    }
}
