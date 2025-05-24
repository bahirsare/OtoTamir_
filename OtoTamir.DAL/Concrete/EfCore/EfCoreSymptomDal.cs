using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreSymptomDal : EfCoreGenericRepositoryDal<Symptom, DataContext>, ISymptomDal
    {
        private readonly DataContext _context;
        public EfCoreSymptomDal(DataContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<int> CreateAsync(Symptom symptom)
        {

            symptom.CreatedDate = DateTime.Now;
            symptom.ModifiedDate = DateTime.Now;

            return await base.CreateAsync(symptom);
        }
        public override async Task<List<Symptom>> GetAllAsync(Expression<Func<Symptom, bool>> filter = null)
        {
            var entities = _context.Symptoms.Include(i => i.ServiceWorkflowLogs).AsQueryable();
            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        }
        public async Task<Symptom> GetOneAsync(int id, string mechanicId = null)
        {
            var query = _context.Symptoms.AsQueryable();

            if (!string.IsNullOrEmpty(mechanicId))
            {
                query = query.Include(s => s.ServiceRecord).
                    ThenInclude(sr => sr.Vehicle).
                    ThenInclude(v => v.Client).
                    ThenInclude(c => c.MechanicId==mechanicId);
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
