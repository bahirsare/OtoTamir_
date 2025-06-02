using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    public interface IMechanicDal : IRepositoryService<Mechanic>
    {
        Task<Mechanic> GetOneAsync(string id);
        Task<int> DeleteAsync(string id);
        Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName);
        string GenerateRandomPassword();
        Task<List<Mechanic>> GetAllAsync(bool includeClient,
            bool includeVehicle,
            Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy,
            Expression<Func<Mechanic, bool>> filter = null);

    }
}
