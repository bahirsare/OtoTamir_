using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<int> DeleteAsync(int id)
        {
            return await _symptomDal.DeleteAsync(id);
        }

        public async Task<Symptom> GetOneAsync(
            string mechanicId,
            int id,
            bool includeVehicle = false,
            bool includeServiceRecord = false)
        {
            return await _symptomDal.GetOneAsync(mechanicId, id, includeVehicle, includeServiceRecord);
        }

        public async Task<List<Symptom>> GetAllAsync(Expression<Func<Symptom, bool>> filter = null)
        {
            return await _symptomDal.GetAllAsync(filter);
        }

        
        public async Task<int> UpdateAsync()
        {
            return await _symptomDal.UpdateAsync();
        }
    }
}
