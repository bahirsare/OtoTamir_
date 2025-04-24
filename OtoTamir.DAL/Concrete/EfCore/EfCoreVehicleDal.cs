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
    public class EfCoreVehicleDal:EfCoreGenericRepositoryDal<Vehicle,DataContext>,IVehicleDal
    {
        private readonly DataContext _context;
        public EfCoreVehicleDal(DataContext context):base(context)
        {
            _context = context;
        }
        public override int Create(Vehicle vehicle)
        {

            vehicle.CreatedDate = DateTime.Now;
            vehicle.ModifiedDate = DateTime.Now;

            return base.Create(vehicle);
        }
        
    }
}
