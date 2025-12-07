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
                   
                    var record = await _serviceRecordService.GetOneAsync(model.ServiceRecordId, model.MechanicId, true, false);
                    if (record == null) throw new Exception("Servis kaydı bulunamadı.");
                    
                    if (model.PaymentMethod == PaymentSource.ClientBalance)
                    {
                        var client = await _clientService.GetOneAsync(record.Vehicle.ClientId, model.MechanicId, false, false);
                     
                        client.Balance += record.Price;
                        await _clientService.UpdateAsync();
                    }
                    // Nakit veya Banka/Kredi Kartı
                    else
                    {
                        var mechanic = await _mechanicService.GetOneAsync(model.MechanicId);
                        if (mechanic.TreasuryId == null)
                        {
                            throw new Exception("Bu kullanıcının tanımlı bir kasası (Treasury) bulunamadı. Lütfen yönetici ile iletişime geçin.");
                        }

                        var transactionRecord = new TreasuryTransaction
                        {
                            TreasuryId = (int)mechanic.TreasuryId,
                            
                            TransactionType = TransactionType.Incoming, // Para Girişi
                            Amount = record.Price,
                            PaymentSource = model.PaymentMethod,
                            BankId = model.PaymentMethod == PaymentSource.Bank ? model.BankId : null,
                            TransactionDate = DateTime.Now,
                            Description = $"{record.Vehicle.Plate} plakalı araç servis ödemesi.",
                            ClientId=record.Vehicle.ClientId,
                            AuthorName=model.AuthorName,
                            
                            
                        }; 


                        
                        await _treasuryTransactionService.AddTransactionAsync(transactionRecord,mechanic.Id);
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
