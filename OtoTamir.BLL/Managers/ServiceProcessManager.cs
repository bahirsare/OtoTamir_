using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly DataContext _context; 

        public ServiceProcessManager(
            IServiceRecordService serviceRecordService,
            ITreasuryTransactionService treasuryTransactionService,
            IClientService clientService,
            IMechanicService mechanicService,
            DataContext context,
            IPosTerminalService posTerminalService,
            IBankService bankService,
            ITreasuryService treasuryService,
            IMapper mapper)
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
        }

        public async Task CompleteServiceProcessAsync(ServiceCompletionDTO model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                
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
                        if (model.PaymentMethod == PaymentSource.CreditCard)
                        {
                            if (model.PosTerminalId == null)
                            {
                                throw new Exception("Kredi kartı ile ödeme seçildiğinde POS cihazı seçmek zorunludur!");
                            }
                        }
                        await _clientService.UpdateBalanceAsync(model.MechanicId, record.Vehicle.ClientId, -record.Price);


                        var payTrx = _mapper.Map<TreasuryTransaction>(model);

                        payTrx.TreasuryId = (int)mechanic.TreasuryId;
                        payTrx.ClientId = record.Vehicle.ClientId;
                        payTrx.TransactionType = TransactionType.Incoming;
                        payTrx.Amount = record.Price;
                        payTrx.TransactionDate = DateTime.Now.AddSeconds(1);
                        payTrx.Description = $"{record.Vehicle.Plate} Servis Ödemesi ({model.PaymentMethod})";

                        await _treasuryTransactionService.AddTransactionAsync(payTrx, mechanic.Id);

                        
                        if (model.PaymentMethod == PaymentSource.Cash)
                        {
                            await _treasuryService.UpdateCashBalanceAsync((int)mechanic.TreasuryId, model.MechanicId, record.Price);
                        }
                        else if (model.PaymentMethod == PaymentSource.Bank && model.BankId != null)
                        {
                            await _bankService.UpdateBalanceAsync((int)model.BankId, model.MechanicId, record.Price);
                        }

                    }

                   
                    await _serviceRecordService.UpdateAsync();
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
                        await _treasuryService.UpdateCashBalanceAsync((int)user.TreasuryId, mechanicId, amount);
                    }

                   
                    else if (paymentSource == PaymentSource.Bank && finalBankId != null)
                    {
                        
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
