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
        Task<decimal> GetTotalBalanceAsync(int treasuryId);
        Task<List<TreasuryTransaction>> GetAllAsync(
        string mechanicId,
        int treasuryId,
        Expression<Func<TreasuryTransaction, bool>> filter = null
        );
        Task<TreasuryTransaction> GetOneAsync(
        int id,
        string mechanicId);
    }
}
