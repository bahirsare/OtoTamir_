using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class ServiceRecordService : IServiceRecordService
    {
        private readonly IServiceRecordDal _serviceRecordDal;
        private readonly ITreasuryTransactionService _treasuryTransactionService;
        private readonly IMechanicService _mechanicService;
        public ServiceRecordService(IServiceRecordDal serviceRecordDal, ITreasuryTransactionService treasuryTransactionService, IMechanicService mechanicService)
        {
            _serviceRecordDal = serviceRecordDal;
            _treasuryTransactionService = treasuryTransactionService;
            _mechanicService = mechanicService;
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
            var record = await _serviceRecordDal.GetOneAsync(id, mechanicId, true, true);
            if (record == null)
                throw new Exception("Servis kaydı bulunamadı.");
            //if (record.Status == "Tamamlandı")
            //{
            //    var mechanic =await _mechanicService.GetOneAsync(mechanicId);
            //    var transaction = new TreasuryTransaction
            //    {
            //        TreasuryId = (int)mechanic.TreasuryId,  // mekanikten alınacak
            //        TransactionType = TransactionType.Incoming,
            //        Amount = record.Price, // örnek: kayıttaki fiyat
            //        PaymentSource = PaymentSource.Cash, // ödeme şekli senin mantığına göre
            //        TransactionDate = DateTime.Now
            //    };

            //    await _treasuryTransactionService.AddTransactionAsync(transaction);
            //}
        }
        public async Task CompleteServiceAsync(ServiceCompletionDTO model)
        {
            // 1. TRANSACTION BAŞLAT (UnitOfWork Mantığı)
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // A. Servis Kaydını Getir
                    var record = await _serviceRecordDal.GetOneAsync(model.ServiceRecordId, model.MechanicId, true, false);
                    if (record == null) throw new Exception("Servis kaydı bulunamadı.");

                    // B. Durumu Güncelle
                    record.Status = "Tamamlandı";
                    record.CompletedDate = DateTime.Now;
                    await _serviceRecordDal.UpdateAsync(); // DB'ye yazar ama Commit edilmezse geri alınır.

                    var mechanic = await _mechanicService.GetOneAsync(model.MechanicId);

                    // C. Ödeme Yöntemine Göre Finansal İşlem
                    if (model.PaymentMethod == PaymentSource.Veresiye) // Örn: Veresiye
                    {
                        // Veresiye: Kasaya para girmez, müşterinin borcu artar.
                        var client = await _clientService.GetOneAsync(record.Vehicle.ClientId, model.MechanicId, false, false);
                        client.Balance -= record.Price; // Bakiye mantığınıza göre (- borç ise)
                        await _clientService.UpdateAsync();
                    }
                    else
                    {
                        // Nakit veya Kart: Kasaya/Bankaya para girer.
                        var transactionRecord = new TreasuryTransaction
                        {
                            TreasuryId = (int)mechanic.TreasuryId,
                            TransactionType = TransactionType.Incoming,
                            Amount = record.Price,
                            PaymentSource = model.PaymentMethod,
                            BankId = model.PaymentMethod == PaymentSource.Bank ? model.BankId : null,
                            TransactionDate = DateTime.Now,
                            Description = $"{record.Vehicle.Plate} plakalı araç servis ödemesi."
                        };

                        // TreasuryTransactionService içinde zaten kasa/banka güncelleme mantığı var.
                        // Ancak orası da ayrı SaveChanges çağırıyor. Transaction içinde olduğu için sorun yok.
                        await _treasuryTransactionService.AddTransactionAsync(transactionRecord);
                    }

                    // 2. HATA YOKSA ONAYLA (COMMIT)
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // 3. HATA VARSA GERİ AL (ROLLBACK)
                    await transaction.RollbackAsync();
                    throw new Exception("İşlem sırasında hata oluştu: " + ex.Message);
                }
            }
        }
    }
}

