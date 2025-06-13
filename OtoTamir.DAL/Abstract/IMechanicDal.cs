using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{  /// <summary>
   /// Provides specialized data access operations for Mechanic entities 
   /// with integrated ASP.NET Core Identity management
   /// </summary>
    public interface IMechanicDal : IRepositoryService<Mechanic>
    {
        /// <summary>
        /// Retrieves a single mechanic by their unique identifier
        /// </summary>
        /// <param name="id">The mechanic's ID (ASP.NET Core Identity compatible)</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the Mechanic entity or null if not found.
        /// </returns>
        Task<Mechanic> GetOneAsync(string id);
        /// <summary>
        /// Permanently deletes a mechanic from both application and identity systems
        /// </summary>
        /// <param name="id">The mechanic's ID to delete</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains 1 if successful, 0 if failed.
        /// </returns>
        Task<int> DeleteAsync(string id);
        /// <summary>
        /// Creates a new mechanic with auto-generated credentials and store setup
        /// </summary>
        /// <param name="storeName">The name of the mechanic's store/workshop</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a tuple with:
        /// - Success status (bool)
        /// - Auto-generated password (string)
        /// - List of error messages if creation failed (List&lt;string&gt;)
        /// </returns>
        /// <remarks>
        /// Automatically sets:
        /// - Username (storeName without spaces, lowercase)
        /// - Default avatar image
        /// - Profile completion flag to false
        /// </remarks>
        Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName);
        /// <summary>
        /// Generates a random 6-character alphanumeric password
        /// </summary>
        /// <returns>Uppercase password containing letters and numbers</returns>
        string GenerateRandomPassword();
        /// <summary>
        /// Retrieves all mechanics with flexible filtering, sorting and relation loading
        /// </summary>
        /// <param name="includeClient">Whether to include related Client entities</param>
        /// <param name="includeVehicle">Whether to include nested Vehicle entities (requires includeClient=true)</param>
        /// <param name="orderBy">Custom sorting function (e.g., q => q.OrderBy(m => m.StoreName))</param>
        /// <param name="filter">Optional filtering expression (e.g., m => m.IsProfileCompleted)</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a list of Mechanic entities.
        /// </returns>
        /// <remarks>
        /// Note: includeVehicle will only work when includeClient is set to true
        /// </remarks>
        Task<List<Mechanic>> GetAllAsync(bool includeClient,
            bool includeVehicle,
            Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy,
            Expression<Func<Mechanic, bool>> filter = null);

    }
}
