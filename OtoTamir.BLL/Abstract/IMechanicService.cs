using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Abstract
{
    public interface IMechanicService : IRepositoryService<Mechanic>
    {
        Task<Mechanic> GetOneAsync(string id);
        Task<int> DeleteAsync(string id);
        Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName);
        string GenerateRandomPassword();
    }
}
