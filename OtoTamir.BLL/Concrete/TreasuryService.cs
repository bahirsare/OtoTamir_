using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context; // DataContext için
using System.Globalization;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class TreasuryService :  ITreasuryService
    {
        private readonly ITreasuryDal _treasuryDal;
        private readonly IBankDal _bankDal;
        private readonly IBankCardDal _bankCardDal;
        private readonly ITreasuryTransactionDal _transactionDal;
        private readonly IClientDal _clientDal;
        private readonly DataContext _context; // Grafik sorgusu için gerekli

        public TreasuryService(
            ITreasuryDal treasuryDal,
            IBankDal bankDal,
            IBankCardDal bankCardDal,
            ITreasuryTransactionDal transactionDal,
            IClientDal clientDal,
            DataContext context) 
        {
            _treasuryDal = treasuryDal;
            _bankDal = bankDal;
            _bankCardDal = bankCardDal;
            _transactionDal = transactionDal;
            _clientDal = clientDal;
            _context = context;
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