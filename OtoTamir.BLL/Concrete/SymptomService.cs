using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class SymptomService : ISymptomService
    {
        private readonly ISymptomDal _symptomDal;

        public SymptomService(ISymptomDal symptomDal)
        {
            _symptomDal = symptomDal;
        }
        public async Task<bool> AnyAsync(Expression<Func<Symptom, bool>> filter)
        {
            return await _symptomDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(Symptom entity)
        {
            return await _symptomDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return  _symptomDal.Delete(id);
        }

        public async Task<Symptom> GetOneAsync(
            string mechanicId,
            int id,
            bool includeVehicle = false,
            bool includeServiceRecord = false)
        {
            return await _symptomDal.GetOneAsync(mechanicId, id, includeVehicle, includeServiceRecord);
        }

        public async Task<List<Symptom>> GetAllAsync(string mechanicId, Expression<Func<Symptom, bool>> filter = null
            )
        {
            return await _symptomDal.GetAllAsync(mechanicId, filter);
        }


        public async Task<int> UpdateAsync()
        {
            return await _symptomDal.UpdateAsync();
        }
    }
}
