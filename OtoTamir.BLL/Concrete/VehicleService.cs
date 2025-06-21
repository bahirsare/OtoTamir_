using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

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

        public  int Delete(int id)
        {
            return  _vehicleDal.Delete(id);
        }

        public async Task<List<Vehicle>> GetAllAsync(string mechanicId, Expression<Func<Vehicle, bool>> filter = null)
        {
            return await _vehicleDal.GetAllAsync(mechanicId, filter);
        }

        public async Task<Vehicle> GetOneAsync(
        string mechanicId,
        bool includeClient,
        bool includeServiceRecords,            
        string plate = null,
        int? id = null
        )
        {
            return await _vehicleDal.GetOneAsync(mechanicId, includeClient, includeServiceRecords,plate,id);
        }
        
        public async Task<int> UpdateAsync()
        {
            return await _vehicleDal.UpdateAsync();
        }

    }
}
