using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Migrations;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class ServiceRecordService : IServiceRecordService
    {
        private readonly IServiceRecordDal _serviceRecordDal;
        private readonly ITreasuryTransactionService _treasuryTransactionService;
        public ServiceRecordService(IServiceRecordDal serviceRecordDal, ITreasuryTransactionService treasuryTransactionService)
        {
            _serviceRecordDal = serviceRecordDal;
            _treasuryTransactionService = treasuryTransactionService;
        }

        public async Task<bool> AnyAsync(Expression<Func<ServiceRecord, bool>> filter)
        {
            return await _serviceRecordDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(ServiceRecord entity)
        {
            return await _serviceRecordDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _serviceRecordDal.Delete(id);
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

        public async Task<ServiceRecord> GetOneAsync(int id, string mechanicId, bool includeVehicle, bool includeSymptoms)
        {
            return await _serviceRecordDal.GetOneAsync(id, mechanicId, includeVehicle, includeSymptoms);
        }


        public async Task<int> UpdateAsync()
        {
            return await _serviceRecordDal.UpdateAsync();

        }
        public async Task UpdateStatusAsync(int id, string mechanicId)
        {
            await _serviceRecordDal.UpdateStatusAsync(id, mechanicId);
            var record = await _serviceRecordDal.GetOneAsync(id, mechanicId, false, true);
            if (record == null)
                throw new Exception("Servis kaydı bulunamadı.");
            if (record.Status == "Tamamlandı")
            {
                var transaction = new TreasuryTransaction
                {
                    TreasuryId = (int)record.Vehicle.Client.Mechanic.TreasuryId,  // mekanikten alınacak
                    TransactionType = TransactionType.Incoming,
                    Amount = record.Price, // örnek: kayıttaki fiyat
                    PaymentSource = PaymentSource.Cash, // ödeme şekli senin mantığına göre
                    TransactionDate = DateTime.Now
                };

                await _treasuryTransactionService.AddTransactionAsync(transaction);
            }
        }
    }
}

