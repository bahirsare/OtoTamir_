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
    public interface IBalanceLogDal : IRepositoryService<BalanceLog>
    {

        Task<BalanceLog> GetOneAsync(
        int id,
        string mechanicId);
        Task<List<BalanceLog>> GetAllAsync(
        string mechanicId,
        int clientId,
        Expression<Func<BalanceLog, bool>> filter = null
        );

    }
}
