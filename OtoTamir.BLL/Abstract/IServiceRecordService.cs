using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface IServiceRecordService : IRepositoryService<ServiceRecord>
    {
        Task<List<ServiceRecord>> GetAllAsync(string mechanicId,
            bool includeVehicle = true,
            bool includeClient = false,
            bool includeSymptoms = false,
            Expression<Func<ServiceRecord, bool>> filter = null
            );
        Task<ServiceRecord> GetOneAsync(int id,
           string mechanicId,
           bool includeVehicle = false,
           bool includeSymptoms = false);
        Task<int> CountByStatusAsync(string mechanicId, ServiceStatus? status,DateTime? date = null);
        Task UpdateStatusAsync(int id, string mechanicId);
        Task<decimal> GetTotalIncomeAsync(string mechanicId, string period);
        Task<List<ServiceRecord>> GetLastRecordsAsync(string mechanicId, int count);

    }

}
