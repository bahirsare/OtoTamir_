using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;

namespace OtoTamir.BLL.Abstract
{
    public interface IVehicleService : IRepositoryService<Vehicle>
    {
        Task<Vehicle> GetOneAsync(string plate = null, int? id = null, string mechanicId = null, bool includeClient = false, bool includeServiceRecords = false);
    }
}
