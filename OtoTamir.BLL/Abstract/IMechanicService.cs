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
        Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName);
        public Mechanic GetOne(string id);
        public int Delete(string id);
    }
}
