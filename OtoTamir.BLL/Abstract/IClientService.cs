using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface IClientService : IRepositoryService<Client>
    {
        Task<List<Client>> GetAllAsync(string mechanicId, Expression<Func<Client, bool>> filter = null);
    }
}
