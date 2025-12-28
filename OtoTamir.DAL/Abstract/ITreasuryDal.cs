using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface ITreasuryDal:IRepositoryService<Treasury>
    {
        Task<decimal> GetTotalBalanceAsync(int treasuryId,string mechanicId);
        Task<List<Treasury>> GetAllAsync(
        string mechanicId,
        int treasuryId,
        Expression<Func<Treasury, bool>> filter = null
        );
        Task<Treasury> GetOneAsync(
        int id,
        string mechanicId);
        Task UpdateCashBalanceAsync(int treasuryId, string mechanicId, decimal amount);
    }
}
