using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    public interface IVehicleDal : IRepositoryService<Vehicle>

    {
        Task<List<Vehicle>> GetAllAsync(
            string mechanicId,            
            Expression<Func<Vehicle, bool>> filter = null);
        Task<Vehicle> GetOneAsync(string mechanicId,
            bool includeClient,
            bool includeServiceRecords,
            string plate,
            int? id );
    }
}
