using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    public interface IVehicleDal : IRepositoryService<Vehicle>

    {   /// <summary>
        /// Retrieves all vehicles associated with a specific mechanic with optional filtering.
        /// Includes service records for each vehicle by default.
        /// </summary>
        /// <param name="mechanicId">The ID of the mechanic (must be a valid GUID or identifier)</param>
        /// <param name="filter">Optional LINQ expression to filter results (e.g., v => v.Brand == "Toyota")</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a list of Vehicle entities.
        /// Returns empty list if no vehicles found for the mechanic.
        /// </returns>
        Task<List<Vehicle>> GetAllAsync(
            string mechanicId,            
            Expression<Func<Vehicle, bool>> filter = null);
        /// <summary>
        /// Retrieves a single vehicle with detailed information using either plate number or database ID.
        /// </summary>
        /// <param name="mechanicId">The ID of the mechanic (authorization check)</param>
        /// <param name="includeClient">If true, includes related Client entity in the result</param>
        /// <param name="includeServiceRecords">If true, includes all related ServiceRecord entities</param>
        /// <param name="plate">Vehicle license plate (case-insensitive, spaces auto-removed). Either plate or id must be provided.</param>
        /// <param name="id">Vehicle database ID. Either plate or id must be provided.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the Vehicle entity 
        /// or null if no matching vehicle found or authorization fails.
        /// </returns>
        Task<Vehicle> GetOneAsync(string mechanicId,
            bool includeClient,
            bool includeServiceRecords,
            string plate,
            int? id );
    }
}
