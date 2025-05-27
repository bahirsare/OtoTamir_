using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Repositories
{
    public interface IRepositoryService<T> where T : class
    {
        /// <summary>
        /// Asynchronously creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> CreateAsync(T entity);

        /// <summary>
        /// Asynchronously saves any pending changes to the database for the tracked entity.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> UpdateAsync();

        /// <summary>
        /// Asynchronously deletes the entity with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Asynchronously checks if any entity exists in the database matching the given filter.
        /// </summary>
        /// <param name="filter">The expression to filter the entities.</param>
        /// <returns>True if any matching entity exists; otherwise, false.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    }
}
