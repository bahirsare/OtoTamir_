using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context; // DataContext için
using System.Globalization;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class TreasuryService :  ITreasuryService
    {
        private readonly ITreasuryDal _treasuryDal;
        private readonly IBankDal _bankDal;
        private readonly IBankCardDal _bankCardDal;
        private readonly ITreasuryTransactionDal _transactionDal;
        private readonly IClientDal _clientDal;
        private readonly DataContext _context; // Grafik sorgusu için gerekli

        public TreasuryService(
            ITreasuryDal treasuryDal,
            IBankDal bankDal,
            IBankCardDal bankCardDal,
            ITreasuryTransactionDal transactionDal,
            IClientDal clientDal,
            DataContext context) 
        {
            _treasuryDal = treasuryDal;
            _bankDal = bankDal;
            _bankCardDal = bankCardDal;
            _transactionDal = transactionDal;
            _clientDal = clientDal;
            _context = context;
        }

        public Task<bool> AnyAsync(Expression<Func<Treasury, bool>> filter)
        {
            return _treasuryDal.AnyAsync(filter);
        }

        public Task<int> CreateAsync(Treasury entity)
        {
            return _treasuryDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _treasuryDal.Delete(id);
        }

        public Task<List<Treasury>> GetAllAsync(string mechanicId, int treasuryId, Expression<Func<Treasury, bool>> filter = null)
        {
            return _treasuryDal.GetAllAsync(mechanicId, treasuryId, filter);
        }

        public async Task<TreasuryDashboardDTO> GetDashboardDataAsync(string mechanicId,int treasuryId)
        {
            var model = new TreasuryDashboardDTO();

            
            var treasury = await _treasuryDal.GetOneAsync(treasuryId,mechanicId);
            if (treasury == null)
            {
                // Eğer kasa yoksa boş bir model dön veya hata fırlatmayıp null bırak
                return new TreasuryDashboardDTO { Treasury = new Treasury() }; 
            }
            model.Treasury = treasury;

            // 2. Banka ve Kartları Getir
            model.Banks = await _bankDal.GetAllAsync(mechanicId);
            model.BankCards = await _bankCardDal.GetAllAsync( mechanicId);

            // 3. Son İşlemleri Getir (Son 10 hareket)
            // Not: Include işlemleri DAL katmanında yapılmamışsa AuthorName boş gelebilir.
            var transactions = await _transactionDal.GetAllAsync(mechanicId,treasury.Id);
            model.Transactions = transactions.OrderByDescending(x => x.TransactionDate).Take(10).ToList();

            // 4. Müşteri Alacaklarını Hesapla (Veresiye Toplamı)
            // Balance > 0 olan tüm müşterileri topla (Borçlu müşteriler)
            var clients = await _clientDal.GetAllAsync(mechanicId,false,false,c => c.Balance > 0);
            model.Treasury.ReceivablesBalance = clients.Sum(c => c.Balance);


            // --- GRAFİK VERİSİ HAZIRLAMA (SENİN SORDUĞUN KISIM) ---
            var endDate = DateTime.Today;
            var startDate = endDate.AddMonths(-5); // Son 6 ay

            // Veritabanından ham veriyi çek (TransactionType ve Ay bazlı grupla)
            // Not: _context.Transactions diyerek doğrudan DB'ye soruyoruz (En performanslısı bu)
            var rawData = await _context.Transactions
                .Where(t => t.TransactionDate >= new DateTime(startDate.Year, startDate.Month, 1) && t.TreasuryId == treasury.Id)
                .GroupBy(t => new { t.TransactionDate.Year, t.TransactionDate.Month, t.TransactionType })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Type = g.Key.TransactionType,
                    Total = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            // Döngü ile son 6 ayı tek tek gez ve boşlukları 0 ile doldur
            for (int i = 5; i >= 0; i--)
            {
                var date = endDate.AddMonths(-i);
                var monthName = date.ToString("MMMM", new CultureInfo("tr-TR"));

                // Labels (Etiketler)
                model.ChartLabels.Add(monthName);

                // Gelir (Income) Bul
                var income = rawData.FirstOrDefault(x => x.Year == date.Year && x.Month == date.Month && x.Type == TransactionType.Incoming);
                model.IncomeData.Add(income?.Total ?? 0);

                // Gider (Outgoing) Bul
                var expense = rawData.FirstOrDefault(x => x.Year == date.Year && x.Month == date.Month && x.Type == TransactionType.Outgoing);
                model.ExpenseData.Add(expense?.Total ?? 0);
            }
            // ---------------------------------

            return model;
        }

        public Task<Treasury> GetOneAsync(int id, string mechanicId)
        {
            return _treasuryDal.GetOneAsync(id, mechanicId);
        }

        public Task<decimal> GetTotalBalanceAsync(int treasuryId, string mechanicId)
        {
            return _treasuryDal.GetTotalBalanceAsync(treasuryId, mechanicId);
        }

        public Task<int> UpdateAsync()
        {
            return _treasuryDal.UpdateAsync();
        }
    }
}