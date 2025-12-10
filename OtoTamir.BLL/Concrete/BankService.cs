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
    public class BankService:IBankService
    {
        private readonly IBankDal _bankDal;

        public BankService(IBankDal bankDal)
        {
            _bankDal = bankDal;
        }
        public async Task<bool> AnyAsync(Expression<Func<Bank, bool>> filter)
        {
            return await _bankDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(Bank entity)
        {
            return await _bankDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _bankDal.Delete(id);
        }

        public Task<List<Bank>> GetAllAsync(string mechanicId, Expression<Func<Bank, bool>> filter = null)
        {
            return _bankDal.GetAllAsync(mechanicId, filter);
        }

        public Task<Bank> GetOneAsync(int id, string mechanicId)
        {
            return _bankDal.GetOneAsync(id, mechanicId);
        }

        public async Task<int> UpdateAsync()
        {
            return await _bankDal.UpdateAsync();
        }
    }
}

