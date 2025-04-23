using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class VehicleService:IVehicleService
    {
        private readonly IVehicleDal _vehicleDal;

        public VehicleService(IVehicleDal vehicleDal)
        {
            _vehicleDal = vehicleDal;
        }

        public int Create(Vehicle Entity)
        {
            return _vehicleDal.Create(Entity);
        }

        public int Delete(int id)
        {
            return _vehicleDal.Delete(id);
        }

        public List<Vehicle> GetAll()
        {
            return _vehicleDal.GetAll();
        }

        public List<Vehicle> GetAll(Expression<Func<Vehicle, bool>> filter = null)
        {
            return _vehicleDal.GetAll(filter);
        }

        public Vehicle GetOne(int id)
        {
            return _vehicleDal.GetOne(id);
        }

        public int Update()
        {
            return _vehicleDal.Update();
        }

    }
}
