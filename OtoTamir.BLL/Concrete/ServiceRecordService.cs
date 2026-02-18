using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using OtoTamir.CORE.Utilities;
using OtoTamir.CORE.Repositories;

namespace OtoTamir.BLL.Concrete
{
    public class ServiceRecordService : IServiceRecordService
    {
        private readonly IServiceRecordDal _serviceRecordDal;
        private readonly ITreasuryTransactionService _treasuryTransactionService;
        private readonly IMechanicService _mechanicService;
        private readonly ILogger<ServiceRecordService> _logger;
        public ServiceRecordService(IServiceRecordDal serviceRecordDal, ITreasuryTransactionService treasuryTransactionService, IMechanicService mechanicService, ILogger<ServiceRecordService> logger)
        {
            _serviceRecordDal = serviceRecordDal;
            _treasuryTransactionService = treasuryTransactionService;
            _mechanicService = mechanicService;
            _logger = logger;
        }

        public async Task<bool> AnyAsync(Expression<Func<ServiceRecord, bool>> filter)
        {
            return await _serviceRecordDal.AnyAsync(filter);
        }

       
        public Task<int> CountByStatusAsync(string mechanicId, ServiceStatus? status,DateTime? date=null)
        {
            
            return _serviceRecordDal.CountByStatusAsync(mechanicId,status,date);
        }

        public async Task<int> CreateAsync(ServiceRecord entity)
        {
            return await _serviceRecordDal.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _serviceRecordDal.DeleteAsync(id);
        }


        public async Task<List<ServiceRecord>> GetAllAsync(string mechanicId,
            bool includeVehicle,
            bool includeClient,
            bool includeSymptoms,
            Expression<Func<ServiceRecord, bool>> filter = null
            )
        {
            return await _serviceRecordDal.GetAllAsync(mechanicId, includeVehicle, includeClient, includeSymptoms, filter);
        }

        public async Task<List<ServiceRecord>> GetLastRecordsAsync(string mechanicId,int count)
        {
            return await _serviceRecordDal.GetLastRecordsAsync(mechanicId, count);
        }

        public async Task<ServiceRecord> GetOneAsync(int id, string mechanicId, bool includeVehicle, bool includeSymptoms)
        {
            return await _serviceRecordDal.GetOneAsync(id, mechanicId, includeVehicle, includeSymptoms);
        }

        public async Task<decimal> GetTotalIncomeAsync(string mechanicId, string period)
        {
            DateTime startDate = DateTime.MinValue;

            if (period == "today") startDate = DateTime.Today;
            else if (period == "month") startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            else if (period == "year") startDate = new DateTime(DateTime.Today.Year, 1, 1);

            return await _serviceRecordDal.GetTotalIncomeAsync(mechanicId, startDate);
        }

        public async Task<int> UpdateAsync()
        {
            return await _serviceRecordDal.UpdateAsync();

        }
        public async Task UpdateStatusAsync(int id, string mechanicId)
        {

            var record = await _serviceRecordDal.GetOneAsync(id, mechanicId, true, true);

            if (record == null)
                throw new Exception("Servis kaydı bulunamadı.");

            var oldStatus = record.Status;

           
            await _serviceRecordDal.UpdateStatusAsync(id, mechanicId);

            
            var updatedRecord = await _serviceRecordDal.GetOneAsync(id, mechanicId, true, false);

            
            if (updatedRecord == null) return;

            var newStatus = updatedRecord.Status;

           
            if (oldStatus != newStatus)
            {
                _logger.LogInformation(
                    "Servis Durumu Değişti. Kayıt: {RecordId} | Eski: {Old} -> Yeni: {New} | Yapan: {User}",
                    id, oldStatus, newStatus, mechanicId
                );
            }
            else
            {
                
                _logger.LogInformation("Servis durumu kontrol edildi, değişiklik olmadı. Kayıt: {RecordId} Durum: {Status}", id, newStatus);
            }
        }

        Task<PagedResult<ServiceRecord>> IRepositoryService<ServiceRecord>.GetPagedAsync(Expression<Func<ServiceRecord, bool>> filter, Func<IQueryable<ServiceRecord>, IOrderedQueryable<ServiceRecord>> orderBy, int page, int pageSize, params Expression<Func<ServiceRecord, object>>[] includes)
        {
           return _serviceRecordDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }
    }
}

