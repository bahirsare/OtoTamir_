
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface IClientDal: IRepositoryService<Client>
    {
        
        Task<List<Client>> GetAllByMechanicAsync(
            string mechanicId,
            Expression<Func<Client, bool>> filter = null,
            bool includeVehicles = false,
            bool includeServiceRecords = false);
        Task<Client> GetOneAsync(int id, string mechanicId = null);
    }
}
