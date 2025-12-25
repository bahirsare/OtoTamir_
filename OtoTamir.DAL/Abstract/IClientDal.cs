
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{

    public interface IClientDal : IRepositoryService<Client>
    {
        /// <summary>
        /// Retrieves a list of clients associated with the specified mechanic.
        /// You can optionally filter the results and include related vehicles and service records.
        /// </summary>
        /// <param name="mechanicId">The ID of the mechanic who owns the clients.</param>
        /// <param name="filter">An optional filter expression for additional filtering.</param>
        /// <param name="includeVehicles">Whether to include the clients' vehicles in the result.</param>
        /// <param name="includeServiceRecords">Whether to include service records for each vehicle (requires includeVehicles to be true).</param>
        Task<List<Client>> GetAllAsync(
            string mechanicId,
            bool includeVehicles,
            bool includeServiceRecords,
            Expression<Func<Client, bool>> filter = null);
        /// <summary>
        /// Retrieves a single client by ID, optionally filtering by mechanic and including related data.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <param name="mechanicId">The ID of the mechanic (used for ownership validation).</param>
        /// <param name="includeVehicles">Whether to include the client's vehicles.</param>
        /// <param name="includeServiceRecords">Whether to include service records for the client's vehicles (requires includeVehicles to be true).</param>
        Task<Client> GetOneAsync(
        int id,
        string mechanicId,
        bool includeVehicles,
        bool includeServiceRecords);
        Task<decimal> GetTotalReceivablesAsync(string mechanidId);

        Task<bool> UpdateBalanceAsync(string mechanicId, int clientId, decimal amount);
    }
}
