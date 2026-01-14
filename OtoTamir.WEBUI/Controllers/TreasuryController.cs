using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using OtoTamir.WEBUI.Services;
using System.Linq.Expressions;

namespace OtoTamir.WEBUI.Controllers
{
    [Authorize]
    public class TreasuryController : Controller
    {
        private readonly ITreasuryService _treasuryService;
        private readonly ITreasuryTransactionService _transactionService;
        private readonly IClientService _clientService;
        private readonly IBankService _bankService;
        private readonly IBankCardService _bankCardService;
        private readonly IPosTerminalService _posTerminalService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly ITransactionCategoryService _categoryService;
        private readonly IMapper _mapper;


        public TreasuryController(
            ITreasuryService treasuryService,
            ITreasuryTransactionService transactionService,
            IClientService clientService,
            IBankService bankService,
            IBankCardService bankCardService,
            IPosTerminalService posTerminalService,
            UserManager<Mechanic> userManager, IMapper mapper, ITransactionCategoryService categoryService)
        {
            _treasuryService = treasuryService;
            _transactionService = transactionService;
            _clientService = clientService;
            _bankService = bankService;
            _bankCardService = bankCardService;
            _posTerminalService = posTerminalService;
            _userManager = userManager;
            _mapper = mapper;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index(
     DateTime? startDate,
     DateTime? endDate,
     int? typeId,
     int? sourceId,
     int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.TreasuryId == null) return RedirectToAction("Profile", "Account");


            if (!startDate.HasValue) startDate = DateTime.Today.AddDays(-30);
            if (!endDate.HasValue) endDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            else endDate = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            Expression<Func<TreasuryTransaction, bool>> filter = x =>

                x.TreasuryId == user.TreasuryId &&
                x.TransactionDate >= startDate &&
                x.TransactionDate <= endDate;

            if (typeId.HasValue) filter = filter.AndAlso(x => x.TransactionType == (TransactionType)typeId.Value);
            if (sourceId.HasValue) filter = filter.AndAlso(x => x.PaymentSource == (PaymentSource)sourceId.Value);


            var pagedResult = await _transactionService.GetPagedAsync(
                filter: filter,
                orderBy: q => q.OrderByDescending(x => x.TransactionDate),
                page: page,
                pageSize: 10,
                includes: x => x.TransactionCategory
            );


            var model = await _treasuryService.GetDashboardDataAsync(user.Id, (int)user.TreasuryId);


            model.Transactions = pagedResult.Results;

            ViewBag.PagedResult = pagedResult;

            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
            ViewBag.SelectedType = typeId;
            ViewBag.SelectedSource = sourceId;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBank(AddBankDTO model)
        {
            var user = await _userManager.GetUserAsync(User);


            var bank = _mapper.Map<Bank>(model);
            bank.TreasuryId = user.TreasuryId;

            var result = await _bankService.CreateAsync(bank);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Banka eklendi.";
            }
            else
            {
                TempData["FailMessage"] = "Banka eklenirken bir hata oluştu.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(AddBankCardDTO model)
        {
            var user = await _userManager.GetUserAsync(User);

            var card = _mapper.Map<BankCard>(model);
            var result = await _bankCardService.CreateAsync(card);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Kart eklendi.";
            }
            else
            {
                TempData["FailMessage"] = "Kart eklenirken bir hata oluştu.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBank(int id)
        {

            var result = await _bankService.DeleteAsync(id);

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Banka hesabı silindi.";
            }
            else
            {
                TempData["FailMessage"] = "Silme işlemi başarısız. Banka bulunamadı veya işlemde hata var.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var result = await _bankCardService.DeleteAsync(id);

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Kart silindi.";
            }
            else
            {
                TempData["FailMessage"] = "Kart silinemedi.";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BankTransactions(int id, DateTime? startDate, DateTime? endDate, int? typeId, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            var bank = await _bankService.GetOneAsync(id,user.Id);
            if (bank == null) return RedirectToAction("Index");

            
            var start = startDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var end = endDate ?? DateTime.Today.AddDays(1).AddSeconds(-1);

            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.SelectedType = typeId;

            
            Expression<Func<TreasuryTransaction, bool>> filter = x =>
                x.Treasury.MechanicId == user.Id &&
                x.BankId == id &&
                x.TransactionDate >= start &&
                x.TransactionDate <= end;

            if (typeId.HasValue)
            {
                filter = filter.AndAlso(x => x.TransactionType == (TransactionType)typeId.Value);
            }

            
            var allTransactions = await _transactionService.GetAllAsync(user.Id,user.TreasuryId,filter);

            var totalIn = allTransactions
                .Where(x => x.TransactionType == TransactionType.Incoming)
                .Sum(x => x.Amount);

            var totalOut = allTransactions
                .Where(x => x.TransactionType == TransactionType.Outgoing)
                .Sum(x => x.Amount);

            int pageSize = 20;
            var pagedResult = await _transactionService.GetPagedAsync(
                filter: filter,
                orderBy: q => q.OrderByDescending(x => x.TransactionDate), 
                page: page,
                pageSize: pageSize,
                includes: x => x.TransactionCategory
            );

           
            decimal startBalanceForPage = bank.Balance;

            if (page > 1)
            {
                var newerTransactions = allTransactions
                    .OrderByDescending(x => x.TransactionDate)
                    .Take((page - 1) * pageSize)
                    .ToList();

                var sumNewer = newerTransactions.Sum(x => x.Amount);
                startBalanceForPage -= sumNewer;
            }
           
            ViewBag.StartingBalance = startBalanceForPage;

            
            if (pagedResult == null)
            {
                pagedResult = new PagedResult<TreasuryTransaction> { Results = new List<TreasuryTransaction>() };
            }
            ViewBag.PagedResult = pagedResult;


            var model = _mapper.Map<BankDetailsDTO>(bank);
           
            model.Transactions = pagedResult.Results != null
                    ? pagedResult.Results.ToList()
                    : new List<TreasuryTransaction>();


            return View(model);
        }




        public async Task<IActionResult> CardTransactions(int id, DateTime? startDate, DateTime? endDate, int? typeId, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            // 1. Kart Bilgisini Çek
            var card = await _bankCardService.GetOneAsync(id ,user.Id);
            if (card == null) return RedirectToAction("Index");

            // Banka Adını Bul (Görsellik için)
            var linkedBank = await _bankService.GetOneAsync(card.BankId,user.Id);
            string bankName = linkedBank != null ? linkedBank.BankName : "Diğer Banka";

            // 2. Filtre Tarihleri
            var start = startDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var end = endDate ?? DateTime.Today.AddDays(1).AddSeconds(-1);

            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.SelectedType = typeId;

            // 3. Sorgu Filtresi
            // Transaction'da BankCardId varsa onu kullanıyoruz
            Expression<Func<TreasuryTransaction, bool>> filter = x =>
              
                
                x.TransactionDate >= start &&
                x.TransactionDate <= end;

            if (typeId.HasValue)
                filter = filter.AndAlso(x => x.TransactionType == (TransactionType)typeId.Value);

            // 4. İstatistikler (Tüm Veri - Limit Doluluğu vs. için değil, ekstre toplamı için)
            // Not: Kartın CurrentDebt (Borç) bilgisi zaten 'card.Balance' içinde var.
            // Biz burada sadece seçilen tarihteki harcama/ödeme toplamını buluyoruz.
            var allTransactions = await _transactionService.GetAllAsync(user.Id,user.TreasuryId,filter);

            // (İsteğe bağlı: View'da bu dönem ne kadar harcadım diye göstermek istersen ViewBag'e atabilirsin)
            // var periodSpending = allTransactions.Where(x => x.TransactionType == TransactionType.Outgoing).Sum(x => x.Amount);

            // 5. Sayfalama
            int pageSize = 20;
            var pagedResult = await _transactionService.GetPagedAsync(
                filter: filter,
                orderBy: q => q.OrderByDescending(x => x.TransactionDate),
                page: page,
                pageSize: pageSize,
                includes: x => x.TransactionCategory
            );

            if (pagedResult == null) pagedResult = new PagedResult<TreasuryTransaction> { Results = new List<TreasuryTransaction>() };

            // 6. Running Balance (Borç Takibi) Başlangıç Hesabı
            decimal startDebtForPage = card.Debt;

            if (page > 1)
            {
                var newerTransactions = allTransactions
                    .OrderByDescending(x => x.TransactionDate)
                    .Take((page - 1) * pageSize)
                    .ToList();

                // Geriye sarma: Şu anki borçtan, harcamaları DÜŞ, ödemeleri EKLE.
                foreach (var t in newerTransactions)
                {
                    if (t.TransactionType == TransactionType.Outgoing)
                        startDebtForPage -= t.Amount;
                    else
                        startDebtForPage += t.Amount;
                }
            }

            // 7. Tarih Hesaplamaları (DTO İçin)
            var today = DateTime.Today;

            // a. Kesim Tarihi Hesapla (Ayın kaçı? 30 çekmeyen aylar için güvenli hesap)
            int safeBillingDay = Math.Min(card.BillingDay, DateTime.DaysInMonth(today.Year, today.Month));
            var nextBillingDay = new DateTime(today.Year, today.Month, safeBillingDay);

            if (nextBillingDay < today) // Bu ayın kesimi geçtiyse, bir sonraki aya at
            {
                nextBillingDay = nextBillingDay.AddMonths(1);
                safeBillingDay = Math.Min(card.BillingDay, DateTime.DaysInMonth(nextBillingDay.Year, nextBillingDay.Month));
                nextBillingDay = new DateTime(nextBillingDay.Year, nextBillingDay.Month, safeBillingDay);
            }

            // b. Son Ödeme Tarihi Hesapla
            int safePaymentDay = Math.Min(card.DueDay, DateTime.DaysInMonth(today.Year, today.Month));
            var nextPayment = new DateTime(today.Year, today.Month, safePaymentDay);

            if (nextPayment < today)
            {
                nextPayment = nextPayment.AddMonths(1);
                safePaymentDay = Math.Min(card.DueDay, DateTime.DaysInMonth(nextPayment.Year, nextPayment.Month));
                nextPayment = new DateTime(nextPayment.Year, nextPayment.Month, safePaymentDay);
            }

            // 8. DTO Doldur
            var model = _mapper.Map<CardDetailsDTO>(card);
            model.DaysLeftToCutOff = (nextBillingDay - today).Days;

            model.Transactions = pagedResult.Results != null ? pagedResult.Results.ToList() : new List<TreasuryTransaction>();
            ViewBag.StartingDebt = startDebtForPage;
            ViewBag.PagedResult = pagedResult;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPosTerminal(AddPosTerminalDTO model)
        {
            var user = await _userManager.GetUserAsync(User);


            var pos = _mapper.Map<PosTerminal>(model);

            var result = await _posTerminalService.CreateAsync(pos);

            if (result > 0)
                TempData["SuccessMessage"] = "POS cihazı başarıyla tanımlandı.";
            else
                TempData["FailMessage"] = "Hata oluştu.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePosTerminal(int id)
        {

            var result = await _posTerminalService.DeleteAsync(id);

            if (result > 0) TempData["SuccessMessage"] = "POS silindi.";
            else TempData["FailMessage"] = "Silinemedi.";

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePosTerminal(EditPosTerminalDTO model)
        {
            var user = await _userManager.GetUserAsync(User);

            // Mevcut kaydı bul
            var pos = await _posTerminalService.GetOneAsync(model.Id, user.Id);
            if (pos == null) return RedirectToAction("Index");

            // Güncelle
            _mapper.Map(model, pos);

            await _posTerminalService.UpdateAsync();

            TempData["SuccessMessage"] = "POS bilgileri güncellendi.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddExpense(AddExpenseDTO model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.TreasuryId == null)
            {
                TempData["FailMessage"] = "Kasa bulunamadı.";
                return RedirectToAction("Index");
            }

            try
            {

                await _transactionService.ProcessExpenseAsync(model, user.Id, (int)user.TreasuryId);

                TempData["SuccessMessage"] = "Harcama başarıyla kaydedildi.";
            }
            catch (Exception ex)
            {
                TempData["FailMessage"] = "Hata: " + ex.Message;
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddCategoryAjax([FromBody] string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = false, message = "Lütfen bir kategori adı giriniz." });
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);

                var category = new TransactionCategory
                {
                    Name = name,
                    MechanicId = user.Id
                };


                await _categoryService.CreateAsync(category);


                return Json(new { success = true, id = category.Id, name = category.Name });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}