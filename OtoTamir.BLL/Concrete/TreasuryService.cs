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
    public class TreasuryService : ITreasuryService
    {
        private readonly ITreasuryDal _treasuryDal;

        public TreasuryService(ITreasuryDal treasuryDal)
        {
            _treasuryDal = treasuryDal;
        }

        public Task<bool> AnyAsync(Expression<Func<Treasury, bool>> filter)
        {
            return _treasuryDal.AnyAsync(filter);
        }

        public Task<int> CreateAsync(Treasury entity)
        {
            return _treasuryDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _treasuryDal.Delete(id);
        }

        public Task<List<Treasury>> GetAllAsync(string mechanicId, int treasuryId, Expression<Func<Treasury, bool>> filter = null)
        {
           return _treasuryDal.GetAllAsync(mechanicId, treasuryId, filter);
        }

        public Task<Treasury> GetOneAsync(int id, string mechanicId)
        {
            return _treasuryDal.GetOneAsync(id, mechanicId);
        }

        public Task<decimal> GetTotalBalanceAsync(int treasuryId, string mechanicId)
        {
            return _treasuryDal.GetTotalBalanceAsync(treasuryId, mechanicId);
        }

        public Task<int> UpdateAsync()
        {
            return _treasuryDal.UpdateAsync();
        }
    }
}
