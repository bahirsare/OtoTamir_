using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class BalanceLogService : IBalanceLogService
    {
        private readonly IBalanceLogDal _balanceLogDal;

        public BalanceLogService(IBalanceLogDal balanceLogDal)
        {
            _balanceLogDal = balanceLogDal;
        }
        public async Task<bool> AnyAsync(Expression<Func<BalanceLog, bool>> filter)
        {
            return await _balanceLogDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(BalanceLog entity)
        {
            return await _balanceLogDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _balanceLogDal.Delete(id);
        }

        public Task<List<BalanceLog>> GetAllAsync(string mechanicId, int clientId, Expression<Func<BalanceLog, bool>> filter = null)
        {
            return _balanceLogDal.GetAllAsync(mechanicId, clientId, filter);
        }

        public Task<BalanceLog> GetOneAsync(int id, string mechanicId)
        {
            return _balanceLogDal.GetOneAsync(id, mechanicId);
        }

        public async Task<int> UpdateAsync()
        {
            return await _balanceLogDal.UpdateAsync();
        }
    }
}
