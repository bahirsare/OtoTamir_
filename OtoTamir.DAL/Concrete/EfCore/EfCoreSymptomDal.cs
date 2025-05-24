using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        }
    }
}
