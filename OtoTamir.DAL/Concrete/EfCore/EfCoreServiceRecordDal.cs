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
        bool includeVehicle,
        bool includeClient,
        bool includeSymptoms,
        Expression<Func<ServiceRecord, bool>> filter = null)
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
            query = query.Include(sr => sr.SymptomList).ThenInclude(s => s.ServiceWorkflowLogs);
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
        bool includeVehicle,
        bool includeSymptoms)
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
            query = query.Include(sr => sr.SymptomList).ThenInclude(s => s.ServiceWorkflowLogs); ;
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
            query = query.Include(sr => sr.SymptomList).ThenInclude(s => s.ServiceWorkflowLogs); ;
        }
        return await query.ToListAsync();
    }


    public async Task<int> CountByStatusAsync(string mechanicId, ServiceStatus? status, DateTime? date = null)
    {
        var query = _context.ServiceRecords
          .Where(sr => sr.Vehicle != null &&
                       sr.Vehicle.Client != null &&
                       sr.Vehicle.Client.MechanicId == mechanicId);
       
        if (status.HasValue)
        {
            query = query.Where(sr => sr.Status == status);
        }
        else if (date.HasValue)
        {
            query = query.Where(sr => sr.CreatedDate >date);
        }
        if (date.HasValue)
        {
            var startDate = date.Value.Date;
            var endDate = startDate.AddDays(1);
            query = query.Where(sr =>
                (sr.CompletedDate >= startDate && sr.CompletedDate < endDate) ||
                (sr.DeletedDate >= startDate && sr.DeletedDate < endDate)
            );
        }

        return await query.CountAsync();
    }
    public async Task UpdateStatusAsync(int id, string mechanicId)
    {
        var record = await GetOneAsync(id, mechanicId, false, true);
        if (record == null)
            throw new Exception("ServiceRecord not found");

        bool anyOngoing = record.SymptomList.Any(s => s.Status == SymptomStatus.Pending);
        bool anyCompleted = record.SymptomList.Any(s => s.Status == SymptomStatus.Fixed);

        if (anyOngoing)
        {
            record.Status = ServiceStatus.InProgress;
        }
        else if (anyCompleted)
        {
            record.Status = ServiceStatus.Completed;
            record.CompletedDate = DateTime.Now;

        }
        else
        {
            record.Status = ServiceStatus.Cancelled;
            record.CompletedDate = DateTime.Now;
        }

        await UpdateAsync();

    }

    public async Task<decimal> GetTotalIncomeAsync(string mechanicId, DateTime startDate)
    {
        return await _context.ServiceRecords
            .Where(sr => sr.Status == ServiceStatus.Completed &&
                         sr.CompletedDate >= startDate &&
                         sr.Vehicle != null &&
                         sr.Vehicle.Client != null &&
                         sr.Vehicle.Client.MechanicId == mechanicId)
            .SumAsync(sr => sr.Price);
    }


    public async Task<List<ServiceRecord>> GetLastRecordsAsync(string mechanicId, int count)
    {
        return await _context.ServiceRecords
            .Include(sr => sr.Vehicle).ThenInclude(v => v.Client)
            .Where(sr => sr.Vehicle != null &&
                         sr.Vehicle.Client != null &&
                         sr.Vehicle.Client.MechanicId == mechanicId)
            .OrderByDescending(sr => sr.CreatedDate)
            .Take(count)
            .ToListAsync();
    }


}
