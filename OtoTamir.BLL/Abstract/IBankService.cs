using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface IBankService : IRepositoryService<Bank>
    {
        Task<Bank> GetOneAsync(
        int id,
        string mechanicId);
        Task<List<Bank>> GetAllAsync(
        string mechanicId,
        ,
        Expression<Func<Bank, bool>> filter = null
        );
    }
}
