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
        Expression<Func<ServiceRecord, bool>> filter = null,
        bool includeVehicle = true,
        bool includeClient = false,
        bool includeSymptoms = false)
    {
        if (string.IsNullOrWhiteSpace(mechanicId))
            throw new ArgumentNullException(nameof(mechanicId));

        var query = _context.ServiceRecords
            .Where(sr => sr.Vehicle != null
                      && sr.Vehicle.Client != null
                      && sr.Vehicle.Client.MechanicId == mechanicId);


        if (includeVehicle)
        {
            query = query.Include(sr => sr.Vehicle);

            if (includeClient)
            {
                query = query.Include(sr => sr.Vehicle.Client);
            }
        }

        if (includeSymptoms)
        {
            query = query.Include(sr => sr.SymptomList);
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
        var query = _context.ServiceRecords
            .Where(sr => sr.Id == id
                      && sr.Vehicle != null
                      && sr.Vehicle.Client != null
                      && sr.Vehicle.Client.MechanicId == mechanicId);

        if (includeVehicle)
        {
            query = query.Include(sr => sr.Vehicle);
        }

        if (includeSymptoms)
        {
            query = query.Include(sr => sr.SymptomList);
        }

        return await query.FirstOrDefaultAsync();
    }
    public async Task<List<ServiceRecord>> GetByVehicleAsync(
       int vehicleId,
       string mechanicId,
       bool includeSymptoms = false)
    {
        var query = _context.ServiceRecords
                   .Where(sr => sr.VehicleId == vehicleId &&
                        sr.Vehicle.Client != null &&
                        sr.Vehicle.Client.MechanicId == mechanicId);
        if (includeSymptoms)
        {
            query = query.Include(sr => sr.SymptomList);
        }
        return await query.ToListAsync();
    }

    public async Task<int> CountByStatusAsync(string mechanicId, string status)
    {
        return await _context.ServiceRecords
            .CountAsync(sr => sr.Status == status &&
                            sr.Vehicle != null &&
                            sr.Vehicle.Client != null &&
                            sr.Vehicle.Client.MechanicId == mechanicId);
    }
}