using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;
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

        public  async Task<int> DeleteAsync(int id)
        {
            return await _vehicleDal.DeleteAsync(id);
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

        Task<PagedResult<Vehicle>> IRepositoryService<Vehicle>.GetPagedAsync(Expression<Func<Vehicle, bool>> filter, Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy, int page, int pageSize, params Expression<Func<Vehicle, object>>[] includes)
        {
           return _vehicleDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }
    }
}
