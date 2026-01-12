using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace OtoTamir.BLL.Concrete
{
    public class TreasuryTransactionService : ITreasuryTransactionService
    {
        private readonly ITreasuryTransactionDal _transactionDal;
        private readonly ITreasuryDal _treasuryDal;
        private readonly IBankDal _bankDal;
        private readonly IBankCardDal _bankCardDal;
        private readonly IClientDal _clientDal;
        private readonly ILogger<TreasuryTransaction> _logger;

        public TreasuryTransactionService(
            ITreasuryTransactionDal transactionDal,
            ITreasuryDal treasuryDal,
            IBankDal bankDal,
            IBankCardDal bankCardDal,
            IClientDal clientDal, ILogger<TreasuryTransaction> logger)
        {
            _transactionDal = transactionDal;
            _treasuryDal = treasuryDal;
            _bankDal = bankDal;
            _bankCardDal = bankCardDal;
            _clientDal = clientDal;
            _logger = logger;
        }

        public async Task AddTransactionAsync(TreasuryTransaction transaction, string mechanicId)
        {
            
            var treasury = await _treasuryDal.GetOneAsync(transaction.TreasuryId, mechanicId);
            if (treasury == null)
                throw new Exception("Kasa bulunamadı.");

            if (transaction.TransactionType == TransactionType.Outgoing && transaction.Amount > 0)
                transaction.Amount *= -1;

         
            switch (transaction.PaymentSource)
            {
                case PaymentSource.Cash:
               
                    treasury.CashBalance += transaction.Amount;

                   
                    await _treasuryDal.UpdateAsync();
                    break;

                case PaymentSource.Bank:
                    if (!transaction.BankId.HasValue)
                        throw new Exception("Banka bilgisi eksik.");

                    var bank = await _bankDal.GetOneAsync(transaction.BankId.Value, mechanicId);
                    if (bank == null)
                        throw new Exception("Banka bulunamadı.");

                  
                    bank.Balance += transaction.Amount;

                    
                    await _bankDal.UpdateAsync();

                   
                    break;

                case PaymentSource.ClientBalance:
                    if (!transaction.ClientId.HasValue)
                        throw new Exception("Müşteri bilgisi eksik.");

                  
                    var client = await _clientDal.GetOneAsync(transaction.ClientId.Value, mechanicId, false, false);
                    if (client == null)
                        throw new Exception("Müşteri bulunamadı.");

                    
                    client.Balance += transaction.Amount;
                    treasury.ReceivablesBalance = await _clientDal.GetTotalReceivablesAsync(mechanicId);
                   
                    await _clientDal.UpdateAsync();
                    break;
            }

            await _transactionDal.CreateAsync(transaction);
        }
        public async Task AddCardTransactionAsync(TreasuryTransaction transaction)
        {
            if (!transaction.BankCardId.HasValue) throw new Exception("Kart bilgisi eksik.");

            var card = await _bankCardDal.GetOneAsync(transaction.BankCardId.Value, transaction.Treasury.MechanicId);
            if (card == null) throw new Exception("Kart bulunamadı.");

            if (transaction.TransactionType == TransactionType.Outgoing)
            {
                // Kart harcaması → borç artar, treasury değişmez
                card.Debt += Math.Abs(transaction.Amount);
            }
            else
            {
                // Kart borcu ödemesi → borç azalır, banka bakiyesi düşer
                card.Debt -= transaction.Amount;
                var bank = await _bankDal.GetOneAsync(transaction.BankId.Value, transaction.Treasury.MechanicId);
                bank.Balance += transaction.Amount;
                await _bankDal.UpdateAsync();
            }

            await _bankCardDal.UpdateAsync();
            await _transactionDal.CreateAsync(transaction);
        }
        public async Task<bool> AnyAsync(Expression<Func<TreasuryTransaction, bool>> filter)
        {
            return await _transactionDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(TreasuryTransaction entity)
        {
            try
            {
                
                var result = await _transactionDal.CreateAsync(entity);

               
                _logger.LogInformation(
                    "Finansal İşlem: {Type} - Miktar: {Amount} TL - KasaID: {TreasuryId} - Açıklama: {Desc}",
                    entity.TransactionType,
                    entity.Amount,
                    entity.TreasuryId,
                    entity.Description
                );

               
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Finansal işlem kaydedilirken hata oluştu! Miktar: {Amount}", entity.Amount);
                throw;
            }

        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _transactionDal.DeleteAsync(id);
        }

        public Task<List<TreasuryTransaction>> GetAllAsync(string mechanicId, int treasuryId, Expression<Func<TreasuryTransaction, bool>> filter = null)
        {
            return _transactionDal.GetAllAsync(mechanicId, treasuryId, filter);
        }

        public async Task<List<TreasuryTransaction>> GetByDateRangeAsync(int treasuryId, string mechanicId, DateTime start, DateTime end)
        {
            return await _transactionDal.GetByDateRangeAsync(treasuryId, mechanicId, start, end);
        }

        public async Task<List<TreasuryTransaction>> GetByPaymentSourceAsync(int treasuryId, string mechanicId, PaymentSource source)
        {
            return await _transactionDal.GetByPaymentSourceAsync(treasuryId, mechanicId, source);
        }

        public Task<TreasuryTransaction> GetOneAsync(int id, string mechanicId)
        {
            return _transactionDal.GetOneAsync(id, mechanicId);
        }

        public async Task<decimal> GetTotalBalanceAsync(int treasuryId)
        {
            return await _transactionDal.GetTotalBalanceAsync(treasuryId);
        }

        public async Task<int> UpdateAsync()
        {
            return await _transactionDal.UpdateAsync();
        }
        public async Task ProcessExpenseAsync(AddExpenseDTO model, string mechanicId, int treasuryId)
        {
            
            var treasury = await _treasuryDal.GetOneAsync(treasuryId, mechanicId);
            if (treasury == null) throw new Exception("Kasa bulunamadı.");

            
            var trx = new TreasuryTransaction
            {
                TreasuryId = treasuryId,
                Amount = model.Amount, 
                Description = model.Description,
                TransactionDate = model.Date,
                TransactionType = TransactionType.Outgoing,
                PaymentSource = model.PaymentSource,
                AuthorName = "Yönetici", 
                
            };

            
            switch (model.PaymentSource)
            {
                case PaymentSource.Cash:
                    
                    if (treasury.CashBalance < model.Amount)
                        throw new Exception($"Kasada yeterli nakit yok! Mevcut: {treasury.CashBalance:C2}");

                    treasury.CashBalance -= model.Amount;

                    
                    await _treasuryDal.UpdateAsync();
                    break;

                case PaymentSource.Bank:
                    if (model.BankId == null) throw new Exception("Banka seçimi zorunludur.");
                    trx.BankId = model.BankId;

                    var bank = await _bankDal.GetOneAsync((int)model.BankId, mechanicId);
                    if (bank == null) throw new Exception("Banka bulunamadı.");

                    if (bank.Balance < model.Amount)
                        throw new Exception($"Banka hesabında yeterli bakiye yok! Mevcut: {bank.Balance:C2}");

                   
                    bank.Balance -= model.Amount;
                    await _bankDal.UpdateAsync();
                    break;

                case PaymentSource.CreditCard:
                    if (model.BankCardId == null) throw new Exception("Kredi kartı seçimi zorunludur.");

                   
                    trx.BankCardId = model.BankCardId;
                   
                    trx.Description += $" (Kart ID: {model.BankCardId})";

                    var card = await _bankCardDal.GetOneAsync((int)model.BankCardId, mechanicId);
                    if (card == null) throw new Exception("Kredi kartı bulunamadı.");

                    
                    card.Debt += model.Amount;
                    await _bankCardDal.UpdateAsync();
                    break;
            }

            await _transactionDal.CreateAsync(trx);
        }
    
    }
}
