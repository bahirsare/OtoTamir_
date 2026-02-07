using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
            card.DueDay = model.DueDate;
            card.BillingDay = model.DueDate + 10;
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

           
            var bank = await _bankService.GetOneAsync(id, user.Id);
            if (bank == null) return RedirectToAction("Index");

            var start = startDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var end = endDate ?? DateTime.Today.AddDays(1).AddSeconds(-1);

            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.SelectedType = typeId;

            
            Expression<Func<TreasuryTransaction, bool>> generalFilter = x =>
                x.Treasury.MechanicId == user.Id &&
                x.BankId == id &&
                x.TransactionDate >= start &&
                x.TransactionDate <= end;

         
            var allTransactions = await _transactionService.GetAllAsync(user.Id, user.TreasuryId, generalFilter);

            var totalIn = allTransactions
                .Where(x => x.TransactionType == TransactionType.Incoming)
                .Sum(x => x.Amount);

            var totalOut = allTransactions
                .Where(x => x.TransactionType == TransactionType.Outgoing)
                .Sum(x => x.Amount);

            var tableFilter = generalFilter;

            if (typeId.HasValue)
            {
                
                tableFilter = tableFilter.AndAlso(x => x.TransactionType == (TransactionType)typeId.Value);
            }

            int pageSize = 20;
            var pagedResult = await _transactionService.GetPagedAsync(
                filter: tableFilter, 
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

                foreach (var item in newerTransactions)
                {
                    if (item.TransactionType == TransactionType.Incoming)
                        startBalanceForPage -= item.Amount; 
                    else
                        startBalanceForPage += item.Amount;
                }
            }

            ViewBag.StartingBalance = startBalanceForPage;

            if (pagedResult == null)
            {
                pagedResult = new PagedResult<TreasuryTransaction> { Results = new List<TreasuryTransaction>() };
            }
            ViewBag.PagedResult = pagedResult;

            var model = _mapper.Map<BankDetailsDTO>(bank);

            // Toplamları Modele Basıyoruz
            model.TotalIncomingLastMonth = totalIn;
            model.TotalOutgoingLastMonth = totalOut;

            model.Transactions = pagedResult.Results != null
                    ? pagedResult.Results.ToList()
                    : new List<TreasuryTransaction>();
            model.CurrentBalance = bank.Balance;
            return View(model);
        }


        public async Task<IActionResult> CardTransactions(int id, DateTime? startDate, DateTime? endDate, int? typeId, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            var card = await _bankCardService.GetOneAsync(id,user.Id);
            if (card == null) return RedirectToAction("Index");

            var linkedBank = await _bankService.GetOneAsync(card.BankId,user.Id);
            string bankName = linkedBank != null ? linkedBank.BankName : "Diğer";

            var start = startDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var end = endDate ?? DateTime.Today.AddDays(1).AddSeconds(-1);

            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.SelectedType = typeId;

             Expression<Func<TreasuryTransaction, bool>> filter = x =>
                x.Treasury.MechanicId == user.Id &&
                x.BankCardId == id  &&
                x.TransactionDate >= start &&
                x.TransactionDate <= end;

            

            if (typeId.HasValue)
            {
                if (typeId.Value == 1) // KULLANICI "HARCAMALAR" SEKMESİNİ SEÇTİ
                {
                    // Sadece Kredi Kartı ile yapılanları getir (Borç Artıranlar)
                    filter = filter.AndAlso(x => x.PaymentSource == PaymentSource.CreditCard);
                }
                else if (typeId.Value == 0) // KULLANICI "ÖDEMELER" SEKMESİNİ SEÇTİ
                {
                    // Kredi Kartı OLMAYANLARI getir (Nakit veya Bankadan Karta ödenenler)
                    filter = filter.AndAlso(x => x.PaymentSource != PaymentSource.CreditCard);
                }
            }


            var allTransactions = await _transactionService.GetAllAsync(user.Id,user.TreasuryId,filter);

           
            int pageSize = 20;
            var pagedResult = await _transactionService.GetPagedAsync(
                filter: filter,
                orderBy: q => q.OrderByDescending(x => x.TransactionDate),
                page: page,
                pageSize: pageSize,
                includes: x => x.TransactionCategory
            );

            
            if (pagedResult == null) pagedResult = new PagedResult<TreasuryTransaction> { Results = new List<TreasuryTransaction>() };

           
            decimal startDebtForPage = card.Debt;
            if (page > 1)
            {
                var newerTransactions = allTransactions.OrderByDescending(x => x.TransactionDate).Take((page - 1) * pageSize).ToList();
                foreach (var t in newerTransactions)
                {
                    if (t.TransactionType == TransactionType.Outgoing) startDebtForPage -= t.Amount;
                    else startDebtForPage += t.Amount;
                }
            }
            ViewBag.StartingDebt = startDebtForPage;
            ViewBag.PagedResult = pagedResult;

            var today = DateTime.Today;
            DateTime GetNextValidDate(int targetDay)
            {
                int daysInThisMonth = DateTime.DaysInMonth(today.Year, today.Month);
                int safeDay = Math.Min(targetDay, daysInThisMonth);
                var dateCandidate = new DateTime(today.Year, today.Month, safeDay);

                if (dateCandidate < today)
                {
                    var nextMonth = today.AddMonths(1);
                    int daysInNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
                    int safeDayNext = Math.Min(targetDay, daysInNextMonth);
                    dateCandidate = new DateTime(nextMonth.Year, nextMonth.Month, safeDayNext);
                }
                return dateCandidate;
            }

            var nextCutOffDate = GetNextValidDate(card.BillingDay);
            var nextPaymentDate = GetNextValidDate(card.DueDay);

            if (nextPaymentDate < nextCutOffDate)
            {
                nextPaymentDate = nextPaymentDate.AddMonths(1);
                int safeDay = Math.Min(card.DueDay, DateTime.DaysInMonth(nextPaymentDate.Year, nextPaymentDate.Month));
                nextPaymentDate = new DateTime(nextPaymentDate.Year, nextPaymentDate.Month, safeDay);
            }

            var daysLeft = (nextCutOffDate - DateTime.Today).Days;
            
            var model = _mapper.Map<CardDetailsDTO>(card);
            model.CutOffDay = nextCutOffDate;
            model.NextPaymentDate = nextPaymentDate;
            model.DaysLeftToCutOff = daysLeft < 0 ? 0 : daysLeft; // Negatif çıkarsa 0 yaz

            model.Transactions = pagedResult.Results.ToList();
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

        [HttpPost]
        public async Task<IActionResult> PayCardDebt(int CardId, decimal Amount, int SourceType, int? SourceBankId, string Description)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user.TreasuryId == null)
            {
                TempData["FailMessage"] = "Kasa bulunamadı.";
                return RedirectToAction("Index");
            }

            if (Amount <= 0)
            {
                TempData["FailMessage"] = "Geçersiz tutar.";
                return RedirectToAction("CardTransactions", new { id = CardId });
            }

            try
            {
                // GÜNCELLENMİŞ ÇAĞRI: SourceBankId parametresi eklendi
                await _transactionService.ProcessCardPaymentAsync(user.Id, user.TreasuryId, CardId, Amount, SourceType, SourceBankId, Description);

                TempData["SuccessMessage"] = "Ödeme başarıyla alındı.";
            }
            catch (Exception ex)
            {
                TempData["FailMessage"] = "Hata: " + ex.Message;
            }

            return RedirectToAction("CardTransactions", new { id = CardId });
        }
        [HttpPost]
        public async Task<IActionResult> AddIncome(AddExpenseDTO model)
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {
                await _transactionService.ProcessIncomeAsync(model, user.Id, (int)user.TreasuryId);
                TempData["SuccessMessage"] = "Gelir girişi başarılı.";
            }
            catch (Exception ex)
            {
                TempData["FailMessage"] = "Hata: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        
        public async Task<IActionResult> AddTransfer(decimal Amount, string Direction, int? SourceBankId, int? TargetBankId, string Description)
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {
                await _transactionService.ProcessTransferAsync(user.Id, (int)user.TreasuryId, Amount, Direction, SourceBankId, TargetBankId, Description);
                TempData["SuccessMessage"] = "Transfer işlemi başarılı.";
            }
            catch (Exception ex)
            {
                TempData["FailMessage"] = "Hata: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExportToExcel(DateTime? startDate, DateTime? endDate, int? typeId, int? sourceId)
        {
            var user = await _userManager.GetUserAsync(User);

          
            var start = startDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var end = endDate ?? DateTime.Today.AddDays(1).AddSeconds(-1);

           
            var transactions = await _transactionService.GetAllAsync(user.Id, (int)user.TreasuryId, x =>
                x.TransactionDate >= start &&
                x.TransactionDate <= end &&
                (!typeId.HasValue || x.TransactionType == (TransactionType)typeId) &&
                (!sourceId.HasValue || (int)x.PaymentSource == sourceId)
            );


            ExcelPackage.License.SetNonCommercialPersonal("Ototamir");

            using (var package = new ExcelPackage())
            {
                // Sayfa ekle
                var ws = package.Workbook.Worksheets.Add("Kasa Hareketleri");

                // --- BAŞLIKLAR ---
                ws.Cells[1, 1].Value = "Tarih";
                ws.Cells[1, 2].Value = "Açıklama";
                ws.Cells[1, 3].Value = "İşlem Yapan";
                ws.Cells[1, 4].Value = "Kategori";
                ws.Cells[1, 5].Value = "Tür";       // Gelir/Gider
                ws.Cells[1, 6].Value = "Kaynak";    // Nakit/Banka
                ws.Cells[1, 7].Value = "Tutar";

                // Başlık Stili (Kalın ve Gri Arka Plan)
                using (var range = ws.Cells[1, 1, 1, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // --- VERİLERİ DOLDUR ---
                int row = 2;
                foreach (var item in transactions.OrderByDescending(x => x.TransactionDate))
                {
                    ws.Cells[row, 1].Value = item.TransactionDate.ToString("dd.MM.yyyy HH:mm");
                    ws.Cells[row, 2].Value = item.Description;
                    ws.Cells[row, 3].Value = item.AuthorName;
                    ws.Cells[row, 4].Value = item.TransactionCategory?.Name ?? "Genel";

                    // Tür (Giriş/Çıkış)
                    ws.Cells[row, 5].Value = item.TransactionType == TransactionType.Incoming ? "Giriş (+)" : "Çıkış (-)";
                    if (item.TransactionType == TransactionType.Incoming) ws.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.Green);
                    else ws.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                    // Kaynak
                    ws.Cells[row, 6].Value = item.PaymentSource.ToString(); // Cash/Bank/CreditCard

                    // Tutar
                    ws.Cells[row, 7].Value = item.Amount;
                    ws.Cells[row, 7].Style.Numberformat.Format = "#,##0.00 ₺"; // Para formatı

                    row++;
                }

                // --- TOPLAM SATIRI (En Alta) ---
                ws.Cells[row, 6].Value = "TOPLAM:";
                ws.Cells[row, 6].Style.Font.Bold = true;

                // Formül ile toplam aldıralım (Giriş - Çıkış karışık olduğu için kodla hesaplamak daha güvenli)
                var totalAmount = transactions.Sum(x => x.TransactionType == TransactionType.Incoming ? x.Amount : -x.Amount);
                ws.Cells[row, 7].Value = totalAmount;
                ws.Cells[row, 7].Style.Font.Bold = true;
                ws.Cells[row, 7].Style.Numberformat.Format = "#,##0.00 ₺";

                // Sütunları içeriğe göre genişlet
                ws.Cells.AutoFitColumns();

                // Dosya adı oluştur
                string fileName = $"KasaRaporu_{start:dd.MM}_{end:dd.MM}.xlsx";

                // İndir
                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }

}