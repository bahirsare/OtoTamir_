using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Concrete.EfCore;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

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

    public async Task<List<Client>> GetAllAsync(
        string mechanicId,
        Expression<Func<Client, bool>> filter = null,
        bool includeVehicles = false,
        bool includeServiceRecords = false)
    {
        var query = _context.Clients
            .Where(c => c.MechanicId == mechanicId);

        query = ApplyIncludes(query, includeVehicles, includeServiceRecords);

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<Client> GetOneAsync(
        int id,
        string mechanicId,
        bool includeVehicles = true,
        bool includeServiceRecords = false)
    {
        var query = _context.Clients.Where(c => c.Id == id&& c.MechanicId == mechanicId);

      

        query = ApplyIncludes(query, includeVehicles, includeServiceRecords);

        return await query.FirstOrDefaultAsync();
    }
    
    private IQueryable<Client> ApplyIncludes(
        IQueryable<Client> query,
        bool includeVehicles,
        bool includeServiceRecords)
    {
        if (includeVehicles)
        {
            query = query.Include(c => c.Vehicles);

            if (includeVehicles)
            {
                var vehicleInclude = query.Include(c => c.Vehicles);

                query = includeServiceRecords
                    ? vehicleInclude.ThenInclude(v => v.ServiceRecords)
                    : vehicleInclude;
            }
        }
        return query;
    }
}