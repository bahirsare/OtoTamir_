using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreClientDal : EfCoreGenericRepositoryDal<Client, DataContext>, IClientDal
    {
        private readonly DataContext _context;

        public EfCoreClientDal(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<int> CreateAsync(Client client)
        {
            client.CreatedDate = DateTime.Now;
            client.ModifiedDate = DateTime.Now;
            return await base.CreateAsync(client);
        }

        public async Task<List<Client>> GetAllByMechanicAsync(
     string mechanicId,
     Expression<Func<Client, bool>> filter = null,
     bool includeVehicles = false,
     bool includeServiceRecords = false)
        {
            var query = _context.Clients
                .Where(c => c.MechanicId == mechanicId);

            if (includeVehicles)
            {
                query = query.Include(c => c.Vehicles);

                if (includeServiceRecords)
                {
                    query = ((IIncludableQueryable<Client, Vehicle>)query)
                           .ThenInclude(v => v.ServiceRecords);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public override Task<List<Client>> GetAllAsync(Expression<Func<Client, bool>> filter = null)
        {
            throw new NotSupportedException();
        }

        public override Task<List<Client>> GetAllAsync()
        {
            throw new NotSupportedException();
        }

        public async Task<Client> GetOneAsync(int id, string mechanicId = null)
        {
            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(mechanicId))
            {
                query = query.Where(c => c.MechanicId == mechanicId);
            }

            return await query
                .Include(c => c.Vehicles)
                .ThenInclude(v => v.ServiceRecords)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        
    }
}