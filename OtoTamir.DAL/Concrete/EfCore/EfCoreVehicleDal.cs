using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Concrete.EfCore;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

public class EfCoreVehicleDal : EfCoreGenericRepositoryDal<Vehicle, DataContext>, IVehicleDal
{
    private readonly DataContext _context;

    public EfCoreVehicleDal(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Vehicle>> GetAllAsync(string mechanicId, Expression<Func<Vehicle, bool>> filter = null)
    {
        var query = _context.Vehicles
            .Include(v => v.ServiceRecords)
            .Where(v => v.Client.MechanicId == mechanicId)
            .AsQueryable();
        if (filter != null)
            query = query.Where(filter);

        return await query.ToListAsync();
    }

    public override async Task<int> CreateAsync(Vehicle vehicle)
    {

        if (string.IsNullOrWhiteSpace(vehicle.Plate))
            throw new ArgumentException("Plaka boş olamaz");

        vehicle.Plate = vehicle.Plate.ToUpper().Replace(" ", "");
        vehicle.Name = $"{vehicle.Plate}_{vehicle.Brand}";
        vehicle.CreatedDate = DateTime.Now;
        vehicle.ModifiedDate = DateTime.Now;

        return await base.CreateAsync(vehicle);
    }

    public async Task<Vehicle> GetOneAsync(
        string plate = null,
        int? id = null,
        string mechanicId = null,
        bool includeClient = false,
        bool includeServiceRecords = false)
    {
        if (plate == null && id == null)
            throw new ArgumentException("Plate veya ID belirtilmeli");

        var query = _context.Vehicles.AsQueryable();


        if (includeClient)
            query = query.Include(v => v.Client);

        if (includeServiceRecords)
            query = query.Include(v => v.ServiceRecords);


        if (!string.IsNullOrEmpty(plate))
            query = query.Where(v => v.Plate == plate.ToUpper().Replace(" ", ""));

        if (id.HasValue)
            query = query.Where(v => v.Id == id);

        if (!string.IsNullOrEmpty(mechanicId))
            query = query.Where(v => v.Client != null && v.Client.MechanicId == mechanicId);

        return await query.FirstOrDefaultAsync();
    }


}