using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    /// <summary>
    /// Interface for accessing and managing Symptom entities in the data layer.
    /// </summary>
    public interface ISymptomDal : IRepositoryService<Symptom>
    {
        /// <summary>
        /// Retrieves a list of symptoms that belong to a specific mechanic, optionally filtered and with related data.
        /// </summary>
        /// <param name="mechanicId">The ID of the mechanic who owns the symptoms.</param>
        /// <param name="filter">Optional expression to filter the results.</param>
        /// <returns>A list of symptoms matching the criteria.</returns>
        Task<List<Symptom>> GetAllAsync(string mechanicId, Expression<Func<Symptom, bool>> filter = null);

        /// <summary>
        /// Retrieves a specific symptom by ID and mechanic ID, with optional inclusion of related vehicle and service record data.
        /// </summary>
        /// <param name="mechanicId">The ID of the mechanic who owns the symptom.</param>
        /// <param name="id">The ID of the symptom to retrieve.</param>
        /// <param name="includeVehicle">Whether to include the related vehicle.</param>
        /// <param name="includeServiceRecord">Whether to include the related service record.</param>
        /// <returns>The symptom matching the specified ID and mechanic, or null if not found.</returns>
        Task<Symptom> GetOneAsync(
            string mechanicId,
            int id,
            bool includeVehicle,
            bool includeServiceRecord);
    }
}
