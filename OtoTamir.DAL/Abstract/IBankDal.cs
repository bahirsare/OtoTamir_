using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    public interface IBankDal : IRepositoryService<Bank>
    {

        Task<Bank> GetOneAsync(
            int id,
            string mechanicId);
        Task<List<Bank>> GetAllAsync(
            string mechanicId,
            int clientId,
            Expression<Func<Bank, bool>> filter = null
        );

    }
}
