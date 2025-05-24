using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class EfCoreVehicleDal : EfCoreGenericRepositoryDal<Vehicle, DataContext>, IVehicleDal
    {
        private readonly DataContext _context;
        public EfCoreVehicleDal(DataContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<List<Vehicle>> GetAllAsync(Expression<Func<Vehicle, bool>> filter = null)
        {
            var entities = _context.Vehicles.Include(i => i.ServiceRecords).AsQueryable();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        }
        public override async Task<int> CreateAsync(Vehicle vehicle)
        {
            vehicle.CreatedDate = DateTime.Now;
            vehicle.ModifiedDate = DateTime.Now;
            vehicle.Plate.ToUpper().Replace(" ", "");
            vehicle.Name=vehicle.Plate+vehicle.Brand;

            return await base.CreateAsync(vehicle);
        }
        public async Task<Vehicle> GetOneAsync(string plate)
        {
            var vehicle = _context.Vehicles.Include(i => i.ServiceRecords).AsQueryable();
          await  vehicle.Where(i=> i.Plate == plate).FirstOrDefaultAsync();
            return (Vehicle)vehicle;
        }
    }

}
