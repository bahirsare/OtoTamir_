using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Exceptions;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
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
        private readonly IMapper _mapper;

        public TreasuryTransactionService(
            ITreasuryTransactionDal transactionDal,
            ITreasuryDal treasuryDal,
            IBankDal bankDal,
            IBankCardDal bankCardDal,
            IClientDal clientDal, ILogger<TreasuryTransaction> logger, IMapper mapper)
        {
            _transactionDal = transactionDal;
            _treasuryDal = treasuryDal;
            _bankDal = bankDal;
            _bankCardDal = bankCardDal;
            _clientDal = clientDal;
            _logger = logger;
            _mapper = mapper;
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

        public async Task<TreasuryTransaction> GetOneAsync(int id, string mechanicId)
        {
            return await _transactionDal.GetOneAsync(id, mechanicId);
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
            var treasury = await _treasuryDal.GetOneAsync(treasuryId, mechanicId)
                ?? throw new NotFoundException("Kasa");

            if (model.Amount <= 0)
                throw new BusinessRuleException("Harcama tutarı sıfırdan büyük olmalıdır.");

            var trx = new TreasuryTransaction
            {
                TreasuryId = treasuryId,
                Amount = model.Amount,
                Description = model.Description+"(Masraf)",
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.Outgoing,
                PaymentSource = (PaymentSource)model.PaymentSource,
                AuthorName = model.Author,
                TransactionCategoryId = model.CategoryId
            };
            
            switch ((PaymentSource)model.PaymentSource)
            {
                case PaymentSource.Cash:
                    if (treasury.CashBalance < model.Amount)
                        throw new BusinessRuleException(
                            $"Kasada yeterli nakit yok! Mevcut: {treasury.CashBalance:C2}");

                    treasury.CashBalance -= model.Amount;
                    await _treasuryDal.UpdateAsync();
                    break;

                case PaymentSource.Bank:
                    if (model.BankId == null)
                        throw new BusinessRuleException("Harcama işlemi için banka seçilmedi.");

                    var bank = await _bankDal.GetOneAsync((int)model.BankId, mechanicId)
                        ?? throw new NotFoundException("Seçilen banka");

                    if (bank.Balance < model.Amount)
                        throw new BusinessRuleException(
                            $"{bank.BankName} hesabında yeterli bakiye yok! Mevcut: {bank.Balance:C2}");

                    trx.BankId = bank.Id;
                    bank.Balance -= model.Amount;
                    await _bankDal.UpdateAsync();
                    break;
                case PaymentSource.CreditCard:
                    
                    if (model.BankCardId == null)
                        throw new BusinessRuleException("Harcama işlemi için kredi kartı seçilmedi.");

                    var creditCard = await _bankCardDal.GetOneAsync((int)model.BankCardId, mechanicId)
                        ?? throw new NotFoundException("Seçilen kredi kartı");

                 
                    if (creditCard.Limit < (creditCard.Debt + model.Amount))
                        throw new BusinessRuleException(
                            $"{creditCard.CardName} kartının limiti bu harcama için yetersiz!");

                    trx.BankCardId = creditCard.Id;
                    
                    creditCard.Debt += model.Amount;

                    await _bankCardDal.UpdateAsync();
                    break;
                default:
                    throw new BusinessRuleException("Geçersiz ödeme kaynağı.");

            }
            await _transactionDal.CreateAsync(trx);
        }
        public async Task ProcessCardPaymentAsync(
             string mechanicId, int treasuryId, int cardId,
             decimal amount, int sourceType, int? sourceBankId, string description)
        {
            var treasury = await _treasuryDal.GetOneAsync((int)treasuryId, mechanicId)
                ?? throw new NotFoundException("Kasa");

            var card = await _bankCardDal.GetOneAsync(cardId, mechanicId)
                ?? throw new NotFoundException("Kart", cardId);

            if (amount > card.Debt)
                throw new BusinessRuleException(
                    $"Ödeme tutarı ({amount:C2}), güncel kart borcundan ({card.Debt:C2}) fazla olamaz!");

            var trx = new TreasuryTransaction
            {
                TreasuryId = (int)treasuryId,
                Amount = amount,
                Description = string.IsNullOrEmpty(description)
                    ? $"{card.CardName} Borç Ödemesi"
                    : description,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.Outgoing,
                PaymentSource = (PaymentSource)sourceType,
                AuthorName = "Yönetici",
                BankCardId = cardId
            };

            switch ((PaymentSource)sourceType)
            {
                case PaymentSource.Cash:
                    if (treasury.CashBalance < amount)
                        throw new BusinessRuleException(
                            $"Kasada yeterli nakit yok! Mevcut: {treasury.CashBalance:C2}");

                    treasury.CashBalance -= amount;
                    await _treasuryDal.UpdateAsync();
                    break;

                case PaymentSource.Bank:
                    if (sourceBankId == null)
                        throw new BusinessRuleException("Ödeme yapılacak banka seçilmedi.");

                    var bank = await _bankDal.GetOneAsync((int)sourceBankId, mechanicId)
                        ?? throw new NotFoundException("Seçilen banka");

                    if (bank.Balance < amount)
                        throw new BusinessRuleException(
                            $"{bank.BankName} hesabında yeterli bakiye yok! Mevcut: {bank.Balance:C2}");

                    trx.BankId = bank.Id;
                    bank.Balance -= amount;
                    await _bankDal.UpdateAsync();
                    break;

                default:
                    throw new BusinessRuleException("Geçersiz ödeme kaynağı.");
            }

            card.Debt -= amount;
            if (card.Debt < 0) card.Debt = 0;

            await _bankCardDal.UpdateAsync();
            await _transactionDal.CreateAsync(trx);
        }

        public async Task ProcessIncomeAsync(AddExpenseDTO model, string mechanicId, int treasuryId)
        {
            var treasury = await _treasuryDal.GetOneAsync(treasuryId, mechanicId)
                ?? throw new NotFoundException("Kasa");

            // Tutar kontrolü
            if (model.Amount <= 0)
                throw new BusinessRuleException("Gelir tutarı sıfırdan büyük olmalıdır.");

            var trx = new TreasuryTransaction
            {
                TreasuryId = treasuryId,
                Amount = model.Amount,
                Description = model.Description,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.Incoming,
                PaymentSource = (PaymentSource)model.PaymentSource,
                AuthorName = model.Author,
                TransactionCategoryId = model.CategoryId
            };

            switch ((PaymentSource)model.PaymentSource)
            {
                case PaymentSource.Cash:
                    treasury.CashBalance += model.Amount;
                    await _treasuryDal.UpdateAsync();
                    break;

                case PaymentSource.Bank:
                    if (model.BankId == null)
                        throw new BusinessRuleException("Gelir işlemi için banka seçilmedi.");

                    var bank = await _bankDal.GetOneAsync((int)model.BankId, mechanicId)
                        ?? throw new NotFoundException("Seçilen banka");

                    trx.BankId = bank.Id;
                    bank.Balance += model.Amount;
                    await _bankDal.UpdateAsync();
                    break;

                default:
                    throw new BusinessRuleException("Geçersiz ödeme kaynağı.");
            }

            await _transactionDal.CreateAsync(trx);
        }


        public async Task ProcessTransferAsync(
            string mechanicId, int treasuryId, decimal amount,
            string direction, int? sourceBankId, int? targetBankId, string description,string author)
        {
            if (amount <= 0)
                throw new BusinessRuleException("Transfer tutarı sıfırdan büyük olmalıdır.");

            var treasury = await _treasuryDal.GetOneAsync(treasuryId, mechanicId)
                ?? throw new NotFoundException("Kasa");

            switch (direction)
            {
                case "ToBank":
                    if (treasury.CashBalance < amount)
                        throw new BusinessRuleException(
                            $"Kasada yeterli nakit yok! Mevcut: {treasury.CashBalance:C2}");

                    if (targetBankId == null)
                        throw new BusinessRuleException("Hedef banka seçilmedi.");

                    var targetBank = await _bankDal.GetOneAsync((int)targetBankId, mechanicId)
                        ?? throw new NotFoundException("Hedef banka");

                    treasury.CashBalance -= amount;
                    targetBank.Balance += amount;
                    await _treasuryDal.UpdateAsync();
                    await _bankDal.UpdateAsync();
                    break;

                case "ToCash":
                    if (sourceBankId == null)
                        throw new BusinessRuleException("Kaynak banka seçilmedi.");

                    var sourceBank = await _bankDal.GetOneAsync((int)sourceBankId, mechanicId)
                        ?? throw new NotFoundException("Kaynak banka");

                    if (sourceBank.Balance < amount)
                        throw new BusinessRuleException(
                            $"{sourceBank.BankName} hesabında yeterli bakiye yok! Mevcut: {sourceBank.Balance:C2}");

                    sourceBank.Balance -= amount;
                    treasury.CashBalance += amount;
                    await _bankDal.UpdateAsync();
                    await _treasuryDal.UpdateAsync();
                    break;

                case "BankToBank":
                    if (sourceBankId == null || targetBankId == null)
                        throw new BusinessRuleException("Kaynak ve hedef banka seçilmedi.");

                    if (sourceBankId == targetBankId)
                        throw new BusinessRuleException("Kaynak ve hedef banka aynı olamaz.");

                    var fromBank = await _bankDal.GetOneAsync((int)sourceBankId, mechanicId)
                        ?? throw new NotFoundException("Kaynak banka");

                    var toBank = await _bankDal.GetOneAsync((int)targetBankId, mechanicId)
                        ?? throw new NotFoundException("Hedef banka");

                    if (fromBank.Balance < amount)
                        throw new BusinessRuleException(
                            $"{fromBank.BankName} hesabında yeterli bakiye yok! Mevcut: {fromBank.Balance:C2}");

                    fromBank.Balance -= amount;
                    toBank.Balance += amount;
                    await _bankDal.UpdateAsync();
                    break;

                default:
                    throw new BusinessRuleException($"Geçersiz transfer yönü: {direction}");
            }

            string turkceAciklama = direction switch
            {
                "ToBank" => "Kasadan Bankaya Para Yatırma",
                "ToCash" => "Bankadan Kasaya Para Çekme",
                "BankToBank" => "Bankalar Arası Virman (Para Transferi)",
                _ => "Transfer İşlemi"
            };

            // Eğer kullanıcı formda özel bir açıklama girdiyse, bizim otomatik yazımızı parantez içinde sona ekleyelim 
            // Örnek: "Ahmet Ustanın maaşı (Kasadan Bankaya Para Yatırma)"
            string finalDescription = string.IsNullOrWhiteSpace(description)
                ? turkceAciklama
                : $"{description} ({turkceAciklama})";

            // 2. Transfer log kaydı
            await _transactionDal.CreateAsync(new TreasuryTransaction
            {
                TreasuryId = treasuryId,
                Amount = amount,
                Description = finalDescription, // 🚀 Türkçe Açıklamamız buraya geldi
                TransactionDate = DateTime.Now,

              
                TransactionType = TransactionType.Outgoing,

                // Hangi transfer olduğuna göre kaynağı doğru ayarlayalım:
                PaymentSource = direction == "ToBank" ? PaymentSource.Cash : PaymentSource.Bank,

                AuthorName = string.IsNullOrWhiteSpace(author) ? "Yönetici" : author
            });
        }
  
        

        async Task<PagedResult<TreasuryTransaction>> IRepositoryService<TreasuryTransaction>.GetPagedAsync(Expression<Func<TreasuryTransaction, bool>> filter, Func<IQueryable<TreasuryTransaction>, IOrderedQueryable<TreasuryTransaction>> orderBy, int page, int pageSize, params Expression<Func<TreasuryTransaction, object>>[] includes)
        {
            return await _transactionDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }

        async Task<int> IRepositoryService<TreasuryTransaction>.RestoreAsync(int id)
        {
            return await _transactionDal.RestoreAsync(id);
        }

        async Task<PagedResult<TreasuryTransaction>> IRepositoryService<TreasuryTransaction>.GetDeletedPagedAsync(Expression<Func<TreasuryTransaction, bool>> filter, Func<IQueryable<TreasuryTransaction>, IOrderedQueryable<TreasuryTransaction>> orderBy, int page, int pageSize, params Expression<Func<TreasuryTransaction, object>>[] includes)
        {
            return await _transactionDal.GetDeletedPagedAsync(filter,orderBy,page, pageSize, includes);
        }
    }
}
