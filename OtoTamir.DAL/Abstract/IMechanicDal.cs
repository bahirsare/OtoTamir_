using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface IMechanicDal: IRepositoryService<Mechanic>
    {
        Task<Mechanic> GetOneAsync(string id);
        Task<int> DeleteAsync(string id);
        Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName);
        string GenerateRandomPassword();
        Task<List<Mechanic>> GetAllAsync(
            Expression<Func<Mechanic, bool>> filter = null,
            bool includeClient = true,
            bool includeVehicle = true,
            Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy = null);

    }
}
