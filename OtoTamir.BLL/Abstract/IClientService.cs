using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface IClientService : IRepositoryService<Client>
    {
        Task<List<Client>> GetAllAsync(string mechanicId, bool includeVehicles=false, bool includeServiceRecords = false, Expression<Func<Client, bool>> filter = null);
        Task<Client> GetOneAsync(
        int id,
        string mechanicId,
        bool includeVehicles = true,
        bool includeServiceRecords = false);
    }
}
