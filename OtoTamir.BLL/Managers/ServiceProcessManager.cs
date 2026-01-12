using AutoMapper;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Context;
using Microsoft.Extensions.Logging;

namespace OtoTamir.BLL.Managers
{
    public class ServiceProcessManager:IServiceProcessManager
    {
        private readonly IServiceRecordService _serviceRecordService;
        private readonly ITreasuryTransactionService _treasuryTransactionService;
        private readonly IClientService _clientService;
        private readonly IMechanicService _mechanicService;
        private readonly IPosTerminalService _posTerminalService;
        private readonly IBankService _bankService;
        private readonly ITreasuryService _treasuryService;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ILogger<ServiceProcessManager> _logger;

        public ServiceProcessManager(
            IServiceRecordService serviceRecordService,
            ITreasuryTransactionService treasuryTransactionService,
            IClientService clientService,
            IMechanicService mechanicService,
            DataContext context,
            IPosTerminalService posTerminalService,
            IBankService bankService,
            ITreasuryService treasuryService,
            IMapper mapper,
            ILogger<ServiceProcessManager> logger)
        {
            _serviceRecordService = serviceRecordService;
            _treasuryTransactionService = treasuryTransactionService;
            _clientService = clientService;
            _mechanicService = mechanicService;
            _context = context;
            _posTerminalService = posTerminalService;
            _bankService = bankService;
            _treasuryService = treasuryService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CompleteServiceProcessAsync(ServiceCompletionDTO model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _logger.LogInformation("Servis tamamlama işlemi başladı. Kayıt ID: {RecordId}, Usta ID: {MechanicId}", model.ServiceRecordId, model.MechanicId);
                    var record = await _serviceRecordService.GetOneAsync(model.ServiceRecordId, model.MechanicId, true, false);
                    if (record == null) throw new Exception("Servis kaydı bulunamadı.");

                    var mechanic = await _mechanicService.GetOneAsync(model.MechanicId);
                    if (mechanic.TreasuryId == null) throw new Exception("Kasa bulunamadı.");

                    
                    record.Status = ServiceStatus.Completed;
                    record.CompletedDate = DateTime.Now;

                    

                    
                    //await _clientService.UpdateBalanceAsync(model.MechanicId, record.Vehicle.ClientId, record.Price);

                   
                    var debtTrx = _mapper.Map<TreasuryTransaction>(model);

                    debtTrx.TreasuryId = (int)mechanic.TreasuryId;
                    debtTrx.ClientId = record.Vehicle.ClientId;
                    debtTrx.TransactionType = TransactionType.Incoming; 
                    debtTrx.Amount = record.Price;
                    debtTrx.PaymentSource = PaymentSource.ClientBalance; 
                    debtTrx.Description = $"{record.Vehicle.Plate} Plakalı Araç Servis Bedeli";

                    debtTrx.BankId = null;
                    debtTrx.PosTerminalId = null;

                    await _treasuryTransactionService.AddTransactionAsync(debtTrx, mechanic.Id);

                   
                    if (model.PaymentMethod != PaymentSource.ClientBalance)
                    {

                        await ReceivePaymentAsync(
                            clientId: record.Vehicle.ClientId,
                            amount: record.Price,
                            description: $"{record.Vehicle.Plate} Servis Ödemesi",
                            paymentSource: model.PaymentMethod,
                            mechanicId: model.MechanicId,
                            authorName: model.AuthorName,
                            posTerminalId: model.PosTerminalId,
                            targetBankId: model.BankId
                        );
                    }

                    // 5. Kaydet ve İşlemi Bitir
                    await _serviceRecordService.UpdateAsync();
                    await transaction.CommitAsync();
                    _logger.LogInformation("Servis başarıyla tamamlandı ve ödeme alındı. Kayıt ID: {RecordId}", model.ServiceRecordId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Servis tamamlama işlemi sırasında bir hata oluştu! Kayıt ID: {RecordId}", model.ServiceRecordId);
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
            var currentTransaction = _context.Database.CurrentTransaction;

           
            var localTransaction = currentTransaction == null ? await _context.Database.BeginTransactionAsync() : null;

            try
            {
                var user = await _mechanicService.GetOneAsync(mechanicId);
                if (user.TreasuryId == null) throw new Exception("Kasa bulunamadı!");

                decimal commissionAmount = 0;
                DateTime maturityDate = DateTime.Now;
                int? finalBankId = null;
                string extraDescription = "";

                // 1. Ödeme Yöntemi Kontrolleri
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
                    if (bank == null) throw new Exception("Seçilen banka hesabı bulunamadı!");

                    finalBankId = targetBankId;
                    maturityDate = DateTime.Now;
                }

                // 2. Tahsilat Kaydı (Incoming Transaction)
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

                // 3. Bakiyeleri Güncelle (Fiziksel Para Hareketi)
                if (paymentSource == PaymentSource.Cash)
                {
                    await _treasuryService.UpdateCashBalanceAsync((int)user.TreasuryId, mechanicId, amount);
                }
                else if (paymentSource == PaymentSource.Bank && finalBankId != null)
                {
                    await _bankService.UpdateBalanceAsync((int)finalBankId, mechanicId, amount);
                }

                // 4. Komisyon Kesintisi (Varsa) - (Outgoing Transaction)
                if (commissionAmount > 0)
                {
                    var expenseTrx = new TreasuryTransaction
                    {
                        TreasuryId = (int)user.TreasuryId,
                        ClientId = null,
                        Amount = commissionAmount,
                        Description = $"POS Komisyon Kesintisi {extraDescription}",
                        TransactionDate = maturityDate, // Vade günü kesilir
                        MaturityDate = maturityDate,
                        TransactionType = TransactionType.Outgoing, // Gider
                        PaymentSource = PaymentSource.Bank, // Bankadan düşer
                        BankId = finalBankId,
                        PosTerminalId = posTerminalId,
                        AuthorName = "Sistem"
                    };
                    await _treasuryTransactionService.CreateAsync(expenseTrx);
                }

                // 5. Müşteri Bakiyesini Düş
                await _clientService.UpdateBalanceAsync(mechanicId, clientId, -amount);

                // FİNAL: Commit İşlemi
                // Eğer transaction'ı biz başlattıysak (localTransaction doluysa), biz bitirelim.
                // Eğer dışarıdan geldiyse (CompleteServiceProcessAsync), dokunmayalım; dışarısı bitirsin.
                if (localTransaction != null)
                {
                    await localTransaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                // Hata olursa ve transaction'ı biz açtıysak geri alalım.
                if (localTransaction != null)
                {
                    await localTransaction.RollbackAsync();
                }
                throw; // Hatayı yukarı fırlat ki dışarıdaki metot da bilsin
            }
            finally
            {
                if (localTransaction != null)
                {
                    await localTransaction.DisposeAsync();
                }
            }
        }
    }
}
