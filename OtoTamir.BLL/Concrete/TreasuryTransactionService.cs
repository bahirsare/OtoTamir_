using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class TreasuryTransactionService : ITreasuryTransactionService
    {
        private readonly ITreasuryTransactionDal _transactionDal;
        private readonly ITreasuryDal _treasuryDal;
        private readonly IBankDal _bankDal;
        private readonly IBankCardDal _bankCardDal;
        private readonly IClientDal _clientDal;

        public TreasuryTransactionService(
            ITreasuryTransactionDal transactionDal,
            ITreasuryDal treasuryDal,
            IBankDal bankDal,
            IBankCardDal bankCardDal,
            IClientDal clientDal)
        {
            _transactionDal = transactionDal;
            _treasuryDal = treasuryDal;
            _bankDal = bankDal;
            _bankCardDal = bankCardDal;
            _clientDal = clientDal;
        }

        public async Task AddTransactionAsync(TreasuryTransaction transaction)
        {
            var treasury = await _treasuryDal.GetOneAsync(transaction.TreasuryId, transaction.Treasury.MechanicId);
            if (treasury == null)
                throw new Exception("Kasa bulunamadı.");

            // Miktar ayarlaması (Outgoing ise negatif, Incoming ise pozitif)
            if (transaction.TransactionType == TransactionType.Outgoing && transaction.Amount > 0)
                transaction.Amount *= -1;

            // PaymentSource'a göre ilgili bakiyeyi güncelle
            switch (transaction.PaymentSource)
            {
                case PaymentSource.Cash:
                    treasury.CashBalance += transaction.Amount;
                    break;

                case PaymentSource.Bank:
                    if (!transaction.BankId.HasValue)
                        throw new Exception("Banka bilgisi eksik.");
                    var bank = await _bankDal.GetOneAsync(transaction.BankId.Value, transaction.Treasury.MechanicId);
                    if (bank == null)
                        throw new Exception("Banka bulunamadı.");
                    bank.Balance += transaction.Amount;
                    await _bankDal.UpdateAsync();
                    treasury.BankBalance += transaction.Amount;
                    break;

                case PaymentSource.ClientBalance:
                    if (!transaction.ClientId.HasValue)
                        throw new Exception("Müşteri bilgisi eksik.");
                    var client = await _clientDal.GetOneAsync(transaction.ClientId.Value, transaction.Treasury.MechanicId, false, false);
                    if (client == null)
                        throw new Exception("Müşteri bulunamadı.");
                    client.Balance += transaction.Amount;
                    await _clientDal.UpdateAsync();
                    treasury.ReceivablesBalance += transaction.Amount;
                    break;
            }

            await _treasuryDal.UpdateAsync();
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
            return await _transactionDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _transactionDal.Delete(id);
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
    }
}
