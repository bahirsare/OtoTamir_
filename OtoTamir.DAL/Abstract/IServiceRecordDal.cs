using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;

namespace OtoTamir.DAL.Abstract
{
    public interface IServiceRecordDal : IRepositoryService<ServiceRecord>
    {
        Task<ServiceRecord> GetOneAsync(int id, string mechanicId = null, bool includeVehicle = false, bool includeSymptoms = false);



    }
}
