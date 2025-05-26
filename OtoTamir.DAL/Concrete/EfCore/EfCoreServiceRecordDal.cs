using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Concrete.EfCore;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

public class EfCoreServiceRecordDal : EfCoreGenericRepositoryDal<ServiceRecord, DataContext>, IServiceRecordDal
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

    public async Task<List<ServiceRecord>> GetAllAsync(
        string mechanicId,
        string status = null,
        Expression<Func<ServiceRecord, bool>> filter = null,
        bool includeClient = false,
        bool includeVehicle = true,
        bool includeSymptoms = false)
    {
        if (string.IsNullOrWhiteSpace(mechanicId))
            throw new ArgumentNullException(nameof(mechanicId));

        var query = _context.ServiceRecords.AsQueryable();

        if (includeClient) { 
            query = query
                .Include(sr => sr.Vehicle)
                .ThenInclude(sr=> sr.Client);
        }
        if (includeVehicle)
        {
            query = query
                .Include(sr => sr.Vehicle);
        }

        if (includeSymptoms)
        {
            query = query.Include(sr => sr.SymptomList);
        }

        // Filtreler
        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(sr => sr.Status == status);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<ServiceRecord> GetOneAsync(
        int id,
        string mechanicId,
        bool includeVehicle = false,
        bool includeSymptoms = false)
    {
        var query = _context.ServiceRecords.AsQueryable();

        if (!string.IsNullOrWhiteSpace(mechanicId))
        {
            query = query
                .Include(sr => sr.Vehicle)
                .ThenInclude(v => v.Client)
                .Where(sr => sr.Vehicle.Client.MechanicId == mechanicId);
        }

        if (includeVehicle)
        {
            query = query.Include(sr => sr.Vehicle);
        }

        if (includeSymptoms)
        {
            query = query.Include(sr => sr.SymptomList);
        }

        return await query.FirstOrDefaultAsync(sr => sr.Id == id);
    }
}