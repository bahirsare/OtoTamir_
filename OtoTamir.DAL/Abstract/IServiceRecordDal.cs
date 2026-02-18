using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    /// <summary>
    /// Data access layer contract for service record operations
    /// </summary>
    public interface IServiceRecordDal : IRepositoryService<ServiceRecord>
    {


        /// <summary>
        /// Retrieves filtered service records with optional includes
        /// </summary>
        /// <param name="mechanicId">Required mechanic ID for authorization</param>
        /// <param name="filter">Additional LINQ filter expression</param>
        /// <param name="includeVehicle">Include related vehicle data (default: true)</param>
        /// <param name="includeClient">Include client data through vehicle (default: false)</param>
        /// <param name="includeSymptoms">Include symptom list (default: false)</param>
        Task<List<ServiceRecord>> GetAllAsync(
            string mechanicId,
            bool includeVehicle,
            bool includeClient,
            bool includeSymptoms,
            Expression<Func<ServiceRecord, bool>> filter = null
            );

        /// <summary>
        /// Gets a single service record with authorization check
        /// </summary>
        /// <param name="id">Service record ID</param>
        /// <param name="mechanicId">Mechanic ID for ownership validation</param>
        /// <param name="includeVehicle">Include vehicle data (default: false)</param>
        /// <param name="includeSymptoms">Include symptom list (default: false)</param>
        Task<ServiceRecord> GetOneAsync(
            int id,
            string mechanicId,
            bool includeVehicle,
            bool includeSymptoms);

        /// <summary>
        /// Gets service records for a specific vehicle with ownership check
        /// </summary>
        /// <param name="vehicleId">Target vehicle ID</param>
        /// <param name="mechanicId">Mechanic ID for authorization</param>
        /// <param name="includeSymptoms">Include symptom list (default: false)</param>
        Task<List<ServiceRecord>> GetByVehicleAsync(
            int vehicleId,
            string mechanicId,
            bool includeSymptoms);

        /// <summary>
        /// Counts records by status for a specific mechanic
        /// </summary>
        Task<int> CountByStatusAsync(string mechanicId, ServiceStatus? status,DateTime? time = null);
        Task UpdateStatusAsync(int id, string mechanicId);
        Task<decimal> GetTotalIncomeAsync(string mechanicId, DateTime startDate);
        Task<List<ServiceRecord>> GetLastRecordsAsync(string mechanicId, int count);
    }
}
