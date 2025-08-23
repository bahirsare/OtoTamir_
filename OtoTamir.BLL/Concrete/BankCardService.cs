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
    public class BankCardService : IBankCardService
    {
        private readonly IBankCardDal _bankCardDal;

        public BankCardService(IBankCardDal bankCardDal)
        {
            _bankCardDal = bankCardDal;
        }

        public async Task<bool> AnyAsync(Expression<Func<BankCard, bool>> filter)
        {
            return await _bankCardDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(BankCard entity)
        {
           return await _bankCardDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
           return _bankCardDal.Delete(id);
        }

        public async Task<List<BankCard>> GetAllAsync(string mechanicId, int treasuryId, Expression<Func<BankCard, bool>> filter = null)
        {
          return await _bankCardDal.GetAllAsync(mechanicId, treasuryId, filter);
        }

        public async Task<BankCard> GetOneAsync(int id, string mechanicId)
        {
            return await _bankCardDal.GetOneAsync(id, mechanicId);
        }

        public async Task<int> UpdateAsync()
        {
           return await _bankCardDal.UpdateAsync();
        }
    }
}
