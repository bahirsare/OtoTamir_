using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
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
    public class EfCoreServiceRecordDal: EfCoreGenericRepositoryDal<ServiceRecord, DataContext>, IServiceRecordDal
    {
        private readonly DataContext _context;

        public EfCoreServiceRecordDal(DataContext context) : base(context)
        {
            _context = context;

        }
        public override async Task<int> CreateAsync(ServiceRecord serviceRecord)
        {

            serviceRecord.CreatedDate = DateTime.Now;
            serviceRecord.ModifiedDate = DateTime.Now;

            return await base.CreateAsync(serviceRecord);
        }
        public override async Task<List<ServiceRecord>> GetAllAsync(Expression<Func<ServiceRecord, bool>> filter = null)
        {
            var entities = _context.ServiceRecords.Include(i=> i.Vehicle).Include(i => i.SymptomList).AsQueryable();
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        }


    }
}
