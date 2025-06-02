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

        public async Task<List<Symptom>> GetAllAsync(string mechanicId,Expression<Func<Symptom, bool>> filter = null)
        {
            var query = _context.Symptoms
                .Include(s => s.ServiceWorkflowLogs).Where(s => s.ServiceRecord.Vehicle.Client.MechanicId == mechanicId).AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
               
        public async Task<Symptom> GetOneAsync(
            string mechanicId,
            int id,
            bool includeVehicle = false,
            bool includeServiceRecord = false)
        {
            var query = _context.Symptoms.AsQueryable();

            if (includeVehicle)
            {
                query = query.Include(s => s.ServiceRecord)
                             .ThenInclude(sr => sr.Vehicle);
            }
            else if (includeServiceRecord)
            {
                query = query.Include(s => s.ServiceRecord);
            }

            query = query.Where(s =>
                s.Id == id &&
                s.ServiceRecord.Vehicle.Client.MechanicId == mechanicId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
