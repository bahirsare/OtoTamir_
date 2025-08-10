using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class TreasuryTransactionService:ITreasuryTransactionService
    {
        private readonly ITreasuryTransactionService _treasuryTransactionService;

        public TreasuryTransactionService(ITreasuryTransactionService treasuryTransactionService)
        {
            _treasuryTransactionService = treasuryTransactionService;
        }
        public async Task<bool> AnyAsync(Expression<Func<TreasuryTransaction, bool>> filter)
        {
            return await _treasuryTransactionService.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(TreasuryTransaction entity)
        {
            return await _treasuryTransactionService.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _treasuryTransactionService.Delete(id);
        }

        public Task<List<TreasuryTransaction>> GetAllAsync(string mechanicId, int clientId, Expression<Func<TreasuryTransaction, bool>> filter = null)
        {
            return _treasuryTransactionService.GetAllAsync(mechanicId, clientId, filter);
        }

        public Task<TreasuryTransaction> GetOneAsync(int id, string mechanicId)
        {
            return _treasuryTransactionService.GetOneAsync(id, mechanicId);
        }

        public async Task<int> UpdateAsync()
        {
            return await _treasuryTransactionService.UpdateAsync();
        }
    }
}
