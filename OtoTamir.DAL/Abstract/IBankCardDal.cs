using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface IBankCardDal:IRepositoryService<BankCard>
    {
        Task<BankCard> GetOneAsync(
           int id,
           string mechanicId);
        Task<List<BankCard>> GetAllAsync(
            string mechanicId,
            int treasuryId,
            Expression<Func<BankCard, bool>> filter = null
        );

    }
}
