using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface IVehicleService : IRepositoryService<Vehicle>
    {
        Task<Vehicle> GetOneAsync(
        string mechanicId,
        bool includeClient=false,
        bool includeServiceRecords = false,
        string plate = null,
        int? id = null
        );
        Task<List<Vehicle>> GetAllAsync(
            string mechanicId,
            Expression<Func<Vehicle, bool>> filter = null);
    }
}
