using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
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
        private readonly IPosTerminalService _posTerminalService;
        private readonly IBankService _bankService;
        private readonly ITreasuryService _treasuryService;
        private readonly DataContext _context; 

        public ServiceProcessManager(
            IServiceRecordService serviceRecordService,
            ITreasuryTransactionService treasuryTransactionService,
            IClientService clientService,
            IMechanicService mechanicService,
            DataContext context,
            IPosTerminalService posTerminalService,
            IBankService bankService,
            ITreasuryService treasuryService)
        {
            _serviceRecordService = serviceRecordService;
            _treasuryTransactionService = treasuryTransactionService;
            _clientService = clientService;
            _mechanicService = mechanicService;
            _context = context;
            _posTerminalService = posTerminalService;
            _bankService = bankService;
            _treasuryService = treasuryService;
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

        public async Task ReceivePaymentAsync(
    int clientId,
    decimal amount,
    string description,
    PaymentSource paymentSource,
    string mechanicId,
    string authorName,
    int? posTerminalId = null,
    int? targetBankId = null 
)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _mechanicService.GetOneAsync(mechanicId);
                    if (user.TreasuryId == null) throw new Exception("Kasa bulunamadı!");

                   
                    decimal commissionAmount = 0;
                    DateTime maturityDate = DateTime.Now;
                    int? finalBankId = null;
                    string extraDescription = "";

                   
                    if (paymentSource == PaymentSource.CreditCard)
                    {
                        if (posTerminalId == null) throw new Exception("Lütfen bir POS cihazı seçiniz!");

                        var pos = await _posTerminalService.GetOneAsync((int)posTerminalId, mechanicId);
                        if (pos == null) throw new Exception("Seçilen POS cihazı bulunamadı veya size ait değil.");

                       
                        maturityDate = DateTime.Now.AddDays(pos.MaturityDays);
                        if (pos.CommissionRate > 0)
                        {
                            commissionAmount = amount * (pos.CommissionRate / 100);
                        }

                        finalBankId = pos.BankId; 
                        extraDescription = $" (POS: {pos.Name})";
                    }

                   
                    else if (paymentSource == PaymentSource.Bank)
                    {
                        if (targetBankId == null) throw new Exception("Lütfen paranın yatacağı bankayı seçiniz!");

                        var bank = await _bankService.GetOneAsync((int)targetBankId, mechanicId);

                        if (bank == null)
                            throw new Exception("Seçilen banka hesabı bulunamadı veya size ait değil! İşlem iptal edildi.");

                        finalBankId = targetBankId;
                        maturityDate = DateTime.Now; 
                    }

                   
                    

                   
                    var incomingTrx = new TreasuryTransaction
                    {
                        TreasuryId = (int)user.TreasuryId,
                        ClientId = clientId,
                        Amount = amount,
                        Description = description + extraDescription,
                        TransactionDate = DateTime.Now,
                        MaturityDate = maturityDate,
                        TransactionType = TransactionType.Incoming,
                        PaymentSource = paymentSource,
                        PosTerminalId = posTerminalId,
                        BankId = finalBankId, 
                        AuthorName = authorName
                    };

                    await _treasuryTransactionService.CreateAsync(incomingTrx);

                    if (paymentSource == PaymentSource.Cash)
                    {
                        // Parametre olarak mechanicId'yi de ekledik
                        await _treasuryService.UpdateCashBalanceAsync((int)user.TreasuryId, mechanicId, amount);
                    }

                    // B) HAVALE İSE -> BANKA BAKİYESİNİ GÜNCELLE
                    else if (paymentSource == PaymentSource.Bank && finalBankId != null)
                    {
                        // Parametre olarak mechanicId'yi de ekledik
                        await _bankService.UpdateBalanceAsync((int)finalBankId, mechanicId, amount);
                    }
                    if (commissionAmount > 0)
                    {
                        var expenseTrx = new TreasuryTransaction
                        {
                            TreasuryId = (int)user.TreasuryId,
                            ClientId = null,
                            Amount = commissionAmount,
                            Description = $"POS Komisyon Kesintisi {extraDescription}",
                            TransactionDate = maturityDate,
                            MaturityDate = maturityDate, 
                            TransactionType = TransactionType.Outgoing,
                            PaymentSource = PaymentSource.Bank, 
                            BankId = finalBankId,
                            PosTerminalId = posTerminalId,
                            AuthorName = "Sistem"
                        };
                        await _treasuryTransactionService.CreateAsync(expenseTrx);
                    }

                  
                    await _clientService.UpdateBalanceAsync(mechanicId, clientId, -amount);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
