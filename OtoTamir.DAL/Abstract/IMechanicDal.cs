using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface IMechanicDal: IRepositoryService<Mechanic>
    {
        public Mechanic GetOne(string id);
        Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName);
        public int Delete(string id);

    }
}
