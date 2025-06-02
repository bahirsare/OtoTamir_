using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Abstract
{
    public interface ISymptomDal : IRepositoryService<Symptom>
    {
        Task<List<Symptom>> GetAllAsync(Expression<Func<Symptom, bool>> filter = null);
        Task<Symptom> GetOneAsync(
            string mechanicId,
            int id,
            bool includeVehicle = false,
            bool includeServiceRecord = false);
    }
}
