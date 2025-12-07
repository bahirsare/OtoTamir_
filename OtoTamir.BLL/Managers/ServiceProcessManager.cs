using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Context;
using System;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Managers
{
    public class ServiceProcessManager
    {
        private readonly IServiceRecordService _serviceRecordService;
        private readonly ITreasuryTransactionService _treasuryTransactionService;
        private readonly IClientService _clientService;
        private readonly IMechanicService _mechanicService;
        private readonly DataContext _context; // Transaction için gerekli

        public ServiceProcessManager(
            IServiceRecordService serviceRecordService,
            ITreasuryTransactionService treasuryTransactionService,
            IClientService clientService,
            IMechanicService mechanicService,
            DataContext context)
        {
            _serviceRecordService = serviceRecordService;
            _treasuryTransactionService = treasuryTransactionService;
            _clientService = clientService;
            _mechanicService = mechanicService;
            _context = context;
        }

        public async Task CompleteServiceProcessAsync(ServiceCompletionDTO model)
        {
            // Transaction başlatıyoruz: Hata olursa her şeyi geri alabilmek için.
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Servis Kaydını Bul
                    var record = await _serviceRecordService.GetOneAsync(model.ServiceRecordId, model.MechanicId, true, false);
                    if (record == null) throw new Exception("Servis kaydı bulunamadı.");
                    if (record.Status == "Tamamlandı") throw new Exception("Bu servis zaten tamamlanmış.");

                    // 2. Servis Durumunu Güncelle (Sadece statü değişir, para işlemi yapılmaz)
                    // Not: Mevcut UpdateStatusAsync metodunun içindeki finansal kodları temizlemen gerekecek (Adım 3'te yapacağız).
                    record.Status = "Tamamlandı";
                    record.CompletedDate = DateTime.Now;
                    await _serviceRecordService.UpdateAsync();

                    // 3. Finansal İşlemleri Yönet
                    // Veresiye (Müşteri Bakiyesinden Düşme)
                    if (model.PaymentMethod == PaymentSource.ClientBalance)
                    {
                        var client = await _clientService.GetOneAsync(record.Vehicle.ClientId, model.MechanicId, false, false);
                        // Müşteri borçlanıyor (Bakiyesi eksiye düşer veya borç hanesi artar)
                        client.Balance -= record.Price;
                        await _clientService.UpdateAsync();
                    }
                    // Nakit veya Banka/Kredi Kartı
                    else
                    {
                        var mechanic = await _mechanicService.GetOneAsync(model.MechanicId);

                        var transactionRecord = new TreasuryTransaction
                        {
                            TreasuryId = (int)mechanic.TreasuryId,
                            TransactionType = TransactionType.Incoming, // Para Girişi
                            Amount = record.Price,
                            PaymentSource = model.PaymentMethod,
                            BankId = model.PaymentMethod == PaymentSource.Bank ? model.BankId : null,
                            TransactionDate = DateTime.Now,
                            Description = $"{record.Vehicle.Plate} plakalı araç servis ödemesi."
                        };

                        // Bu servis zaten bakiyeleri güncelliyor
                        await _treasuryTransactionService.AddTransactionAsync(transactionRecord);
                    }

                    // Hata yoksa onayla
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Hata varsa geri al
                    await transaction.RollbackAsync();
                    throw; // Hatayı yukarı fırlat ki kullanıcıya mesaj gösterebilelim
                }
            }
        }
    }
}
