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
        public  async Task<List<Symptom>> GetAllAsync(Expression<Func<Symptom, bool>> filter = null)
        {
            var entities = _context.Symptoms.Include(i => i.ServiceWorkflowLogs).AsQueryable();
            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            return await entities.ToListAsync();
        }
        public async Task<Symptom> GetOneAsync(int id, string mechanicId = null)
        {
            var query = _context.Symptoms.AsQueryable();

            if (!string.IsNullOrEmpty(mechanicId))
            {
                query = query.Include(s => s.ServiceRecord).
                    ThenInclude(sr => sr.Vehicle).
                    ThenInclude(v => v.Client).
                    ThenInclude(c => c.MechanicId == mechanicId);
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Symptom> GetOneAsync(
        string mechanicId,
        int id,
        bool includeVehicle = false,
        bool includeServiceRecords = false)
        {
            if (id == null)
                throw new ArgumentException("ID belirtilmeli");

            var query = _context.Symptoms.AsQueryable();


            if (includeVehicle)
                query = query.Include(s => s.ServiceRecord).ThenInclude(sr => sr.Vehicle);

            if (includeServiceRecords)
                query = query.Include(s => s.ServiceRecord);

            
                query = query.Where(v => v.Id == id);

            
               

            return await query.FirstOrDefaultAsync(s => s.ServiceRecord.Vehicle.Client != null && s.ServiceRecord.Vehicle.Client.MechanicId == mechanicId&& s.Id == id);
        }
    }
}
