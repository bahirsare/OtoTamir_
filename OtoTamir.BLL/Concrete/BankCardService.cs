using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class BankCardService : IBankCardService
    {
        private readonly IBankCardService _bankCardService;

        public BankCardService(IBankCardService bankCardService)
        {
            _bankCardService = bankCardService;
        }

        public async Task<bool> AnyAsync(Expression<Func<BankCard, bool>> filter)
        {
            return await _bankCardService.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(BankCard entity)
        {
           return await _bankCardService.CreateAsync(entity);
        }

        public int Delete(int id)
        {
           return _bankCardService.Delete(id);
        }

        public async Task<List<BankCard>> GetAllAsync(string mechanicId, int treasuryId, Expression<Func<BankCard, bool>> filter = null)
        {
          return await _bankCardService.GetAllAsync(mechanicId, treasuryId, filter);
        }

        public async Task<BankCard> GetOneAsync(int id, string mechanicId)
        {
            return await _bankCardService.GetOneAsync(id, mechanicId);
        }

        public async Task<int> UpdateAsync()
        {
           return await _bankCardService.UpdateAsync();
        }
    }
}
