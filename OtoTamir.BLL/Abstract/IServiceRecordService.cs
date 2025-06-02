using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Abstract
{
    public interface IServiceRecordService:IRepositoryService<ServiceRecord>
    {
        Task<List<ServiceRecord>> GetAllAsync(string mechanicId,
            bool includeVehicle = true,
            bool includeClient = false,
            bool includeSymptoms = false,            
            Expression<Func<ServiceRecord, bool>> filter = null
            );
        Task<ServiceRecord> GetOneAsync(int id, 
           string mechanicId,
           bool includeVehicle=false, 
           bool includeSymptoms=false);
    }
}
