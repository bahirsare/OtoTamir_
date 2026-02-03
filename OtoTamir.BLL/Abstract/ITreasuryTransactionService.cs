using OtoTamir.CORE.DTOs.TreasuryDTOs;
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
        Task<List<TreasuryTransaction>> GetByPaymentSourceAsync(int treasuryId, string mechanicId, PaymentSource source);
        Task<List<TreasuryTransaction>> GetByDateRangeAsync(int treasuryId, string mechanicId, DateTime start, DateTime end);
        Task AddCardTransactionAsync(TreasuryTransaction transaction);
        Task AddTransactionAsync(TreasuryTransaction transaction,string mechanicId);
        Task ProcessExpenseAsync(AddExpenseDTO model, string mechanicId, int treasuryId);
        Task ProcessCardPaymentAsync(string mechanicId, int treasuryId, int cardId, decimal amount, int sourceType, int? sourceBankId, string description);
        Task ProcessTransferAsync(string mechanicId, int treasuryId, decimal amount, string direction, int? sourceBankId, int? targetBankId, string description);
        Task ProcessIncomeAsync(AddExpenseDTO model, string mechanicId, int treasuryId);
    }
}
