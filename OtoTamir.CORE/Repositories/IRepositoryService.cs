using OtoTamir.CORE.Utilities;
using System.Linq.Expressions;

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
        /// Deletes the entity with the specified ID from the database.
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
        /// <summary>
        /// Belirtilen filtreleme, sıralama ve sayfalama kriterlerine göre verileri asenkron olarak getirir.
        /// İlişkili tabloları (Navigation Properties) eager loading ile dahil edebilir.
        /// </summary>
        /// <param name="filter">
        /// (Opsiyonel) Verileri filtrelemek için LINQ ifadesi. Null geçilirse tüm veriler getirilir.
        /// </param>
        /// <param name="orderBy">
        /// (Opsiyonel) Verileri sıralamak için kullanılacak fonksiyon. Sayfalamanın tutarlı çalışması için verilmesi önerilir.
        /// <br />Örn: <c>q => q.OrderByDescending(x => x.CreatedDate)</c>
        /// </param>
        /// <param name="page">Getirilecek sayfa numarası. (Varsayılan: 1)</param>
        /// <param name="pageSize">Sayfa başına gösterilecek kayıt sayısı. (Varsayılan: 10)</param>
        /// <param name="includes">
        /// (Opsiyonel) Sorguya dahil edilecek ilişkili tablolar (Join işlemi).
        /// </param>
        /// <returns>
        /// İstenen sayfadaki kayıtları (<c>Results</c>) ve toplam kayıt bilgisini (<c>RowCount</c>) içeren <see cref="PagedResult{T}"/> nesnesi döner.
        /// </returns>
        Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int page = 1, int pageSize = 10, params Expression<Func<T, object>>[] includes);
    }
}
