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
        private readonly ISymptomDal _symptomtDal;

        public SymptomService(ISymptomDal symptomtDal)
        {
            _symptomtDal = symptomtDal;
        }
        public async Task<bool> AnyAsync(Expression<Func<Symptom, bool>> filter)
        {
            return await _symptomtDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(Symptom entity)
        {
            return await _symptomtDal.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _symptomtDal.DeleteAsync(id);
        }

        public async Task<List<Symptom>> GetAllAsync()
        {
            return await _symptomtDal.GetAllAsync();
        }

        public async Task<List<Symptom>> GetAllAsync(Expression<Func<Symptom, bool>> filter = null)
        {
            return await _symptomtDal.GetAllAsync(filter);
        }

        public async Task<Symptom> GetOneAsync(int id)
        {
            return await _symptomtDal.GetOneAsync(id);
        }

        public async Task<int> UpdateAsync()
        {
            return await _symptomtDal.UpdateAsync();
        }
    }
}
