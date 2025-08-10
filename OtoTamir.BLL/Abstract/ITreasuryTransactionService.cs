using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface ITreasuryTransactionService : IRepositoryService<TreasuryTransaction>
    {
        Task<List<TreasuryTransaction>> GetAllAsync(
          string mechanicId,
          int treasuryId,
          Expression<Func<TreasuryTransaction, bool>> filter = null
          );
        Task<TreasuryTransaction> GetOneAsync(
        int id,
        string mechanicId);
        Task<decimal> GetTotalBalanceAsync(int treasuryId);
    }
}
