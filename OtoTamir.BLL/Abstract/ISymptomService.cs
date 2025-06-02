using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Abstract
{
    public interface ISymptomService:IRepositoryService<Symptom>
    {
        Task<List<Symptom>> GetAllAsync(Expression<Func<Symptom, bool>> filter = null);
        Task<Symptom> GetOneAsync(
            string mechanicId,
            int id,
            bool includeVehicle = false,
            bool includeServiceRecord = false);
    }
}
