using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Abstract
{
    public interface IMechanicService : IRepositoryService<Mechanic>
    {
        Task<Mechanic> GetOneAsync(string id);
        Task<int> DeleteAsync(string id);
        Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName);
        string GenerateRandomPassword();
        Task<List<Mechanic>> GetAllAsync(
            bool includeClient = true,
            bool includeVehicle= true,                        
            Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy = null,
            Expression<Func<Mechanic, bool>> filter = null);
        Task<bool> RestoreMechanicAsync(string id);

    }
}
