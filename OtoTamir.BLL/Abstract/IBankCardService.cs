using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface IBankCardService : IRepositoryService<BankCard>
    {
        Task<BankCard> GetOneAsync(
         int id,
         string mechanicId);
        Task<List<BankCard>> GetAllAsync(
            string mechanicId,
            Expression<Func<BankCard, bool>> filter = null
        );
    }
}
