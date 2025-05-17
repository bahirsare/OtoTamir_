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
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleDal _vehicleDal;

        public VehicleService(IVehicleDal vehicleDal)
        {
            _vehicleDal = vehicleDal;
        }

        public async Task<bool> AnyAsync(Expression<Func<Vehicle, bool>> filter)
        {
            return await _vehicleDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(Vehicle entity)
        {
            return await _vehicleDal.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _vehicleDal.DeleteAsync(id);
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _vehicleDal.GetAllAsync();
        }

        public async Task<List<Vehicle>> GetAllAsync(Expression<Func<Vehicle, bool>> filter = null)
        {
            return await _vehicleDal.GetAllAsync(filter);
        }

        public async Task<Vehicle> GetOneAsync(int id)
        {
            return await _vehicleDal.GetOneAsync(id);
        }
        public async Task<Vehicle> GetOneAsync(string plate)
        {
            return await _vehicleDal.GetOneAsync(plate);
        }
        public async Task<int> UpdateAsync()
        {
            return await _vehicleDal.UpdateAsync();
        }

    }
}
