using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

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

        public async Task<int> DeleteAsync(int id)
        {
            return await _bankCardDal.DeleteAsync(id);
        }

        public async Task<List<BankCard>> GetAllAsync(string mechanicId, Expression<Func<BankCard, bool>> filter = null)
        {
            return await _bankCardDal.GetAllAsync(mechanicId, filter);
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
