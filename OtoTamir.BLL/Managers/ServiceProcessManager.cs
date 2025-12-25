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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Gerekli verileri çekelim
                    var record = await _serviceRecordService.GetOneAsync(model.ServiceRecordId, model.MechanicId, true, false);
                    if (record == null) throw new Exception("Servis kaydı bulunamadı.");

                    var mechanic = await _mechanicService.GetOneAsync(model.MechanicId);
                    if (mechanic.TreasuryId == null)
                    {
                        throw new Exception("Bu kullanıcının tanımlı bir kasası (Treasury) bulunamadı. Lütfen yönetici ile iletişime geçin.");
                    }

                    // 2. Eğer Ödeme Türü "Veresiye/Bakiye" ise -> Müşterinin Borcunu Arttır
                    if (model.PaymentMethod == PaymentSource.ClientBalance)
                    {
                        var client = await _clientService.GetOneAsync(record.Vehicle.ClientId, model.MechanicId, false, false);
                        client.Balance += record.Price;
                        await _clientService.UpdateAsync();
                    }

                    // 3. İŞLEM KAYDI OLUŞTURMA (ARTIK HER DURUMDA ÇALIŞACAK)
                    // Nakit de olsa, Banka da olsa, Veresiye de olsa bu kayıt oluşsun ki geçmişte görelim.
                    var transactionRecord = new TreasuryTransaction
                    {
                        TreasuryId = (int)mechanic.TreasuryId,
                        TransactionType = TransactionType.Incoming, // Satış/Gelir işlemi
                        Amount = record.Price,
                        PaymentSource = model.PaymentMethod, // Nakit, Banka veya Bakiye olarak kaydolacak
                        BankId = model.PaymentMethod == PaymentSource.Bank ? model.BankId : null,
                        TransactionDate = DateTime.Now,
                        Description = $"{record.Vehicle.Plate} plakalı araç servis ödemesi.",
                        ClientId = record.Vehicle.ClientId,
                        AuthorName = model.AuthorName
                    };

                    // 4. Kaydı veritabanına işle
                    // ÖNEMLİ NOT: AddTransactionAsync metodun, "ClientBalance" geldiğinde 
                    // kasadaki nakit parayı (Amount) arttırmayacak şekilde ayarlanmış olmalı!
                    await _treasuryTransactionService.AddTransactionAsync(transactionRecord, mechanic.Id);

                    // Her şey yolundaysa onayla
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task ReceivePaymentAsync(int clientId, decimal amount, string description, PaymentSource paymentSource, string mechanicId, string authorName)
        {
            // Veri tutarlılığı için Transaction başlatıyoruz
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Ustayı ve Kasasını Bul
                    var user = await _mechanicService.GetOneAsync(mechanicId);
                    if (user.TreasuryId == null)
                        throw new Exception("Kasa bulunamadı!");

                    // 2. Transaction (Kasa Hareketi) Oluştur
                    var treasuryTransaction = new TreasuryTransaction
                    {
                        TreasuryId = (int)user.TreasuryId,
                        ClientId = clientId,
                        Amount = amount,
                        Description = string.IsNullOrEmpty(description) ? "Cari Hesap Ödemesi" : description,
                        TransactionDate = DateTime.Now,
                        TransactionType = TransactionType.Incoming, // Para Girişi
                        PaymentSource = paymentSource,
                        AuthorName = authorName
                    };

                    // Servis üzerinden ekle (Bu servis kendi içinde bakiyeyi güncelliyor mu kontrol etmeliyiz, 
                    // eğer güncellemiyorsa aşağıda biz güncelleriz.)
                    await _treasuryTransactionService.CreateAsync(treasuryTransaction);

                    // 3. Müşteri Bakiyesini Düş (Ödeme alındığı için borç azalır)
                    // UpdateBalanceAsync metoduna EKSİ (-) gönderiyoruz çünkü o += yapıyor.
                    await _clientService.UpdateBalanceAsync(mechanicId, clientId, -amount);

                    // Her şey başarılıysa onayla
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Hata olursa her şeyi geri al
                    await transaction.RollbackAsync();
                    throw; // Hatayı yukarı fırlat ki Controller yakalasın
                }
            }
        }
    }
}
