using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class ServiceRecordService : IServiceRecordService
    {
        private readonly IServiceRecordDal _serviceRecordDal;
        public ServiceRecordService(IServiceRecordDal serviceRecordDal)
        {
            _serviceRecordDal = serviceRecordDal;
        }

        public async Task<bool> AnyAsync(Expression<Func<ServiceRecord, bool>> filter)
        {
            return await _serviceRecordDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(ServiceRecord entity)
        {
            return await _serviceRecordDal.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _serviceRecordDal.DeleteAsync(id);
        }

        public async Task<List<ServiceRecord>> GetAllAsync()
        {
            return await _serviceRecordDal.GetAllAsync();
        }

        public async Task<List<ServiceRecord>> GetAllAsync(Expression<Func<ServiceRecord, bool>> filter = null)
        {
            return await _serviceRecordDal.GetAllAsync(filter);
        }

        public async Task<ServiceRecord> GetOneAsync(int id,string mechanicId)
        {
            return await _serviceRecordDal.GetOneAsync(id, mechanicId);
        }
       

        public async Task<int> UpdateAsync()
        {
            return await _serviceRecordDal.UpdateAsync();
        }
    }
}

