using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;
using OtoTamir.WEBUI.Services;

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
        private readonly UserManager<Mechanic> _userManager;
        private readonly IMapper _mapper;


        public TreasuryController(
            ITreasuryService treasuryService,
            ITreasuryTransactionService transactionService,
             IClientService clientService,
            IBankService bankService,
            IBankCardService bankCardService,
            UserManager<Mechanic> userManager, IMapper mapper)
        {
            _treasuryService = treasuryService;
            _transactionService = transactionService;
            _clientService = clientService;
            _bankService = bankService;
            _bankCardService = bankCardService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        //public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null) return RedirectToAction("Login", "Account");

        //    // Kasa Kontrolü (Daha önce yazdığımız FixMissingTreasury mantığı burada da işe yarar)
        //    if (user.TreasuryId == null)
        //    {
        //        TempData["FailMessage"] = "Kasa bulunamadı. Lütfen önce kasanızı oluşturun.";
        //        return RedirectToAction("FixMissingTreasury", "Account");
        //    }
        //    var start = startDate ?? DateTime.Now.AddDays(-30);
        //    var end = endDate ?? DateTime.Now.AddDays(1);
        //    var treasury = await _treasuryService.GetOneAsync((int)user.TreasuryId, user.Id);

        //    var model = new TreasuryDashboardViewModel
        //    {
        //        Treasury = treasury,
        //        // Listeleri dolduruyoruz
        //        Banks = await _bankService.GetAllAsync(user.Id),
        //        BankCards = await _bankCardService.GetAllAsync(user.Id),


        //        // Son işlemleri tarihe göre sıralı getir
        //        Transactions = (await _transactionService.GetByDateRangeAsync(treasury.Id, user.Id, start, end))
        //                        .OrderByDescending(t => t.TransactionDate)
        //                        .Take(50)
        //                        .ToList()
        //    };

        //    return View(model);
        //}
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var user = await _userManager.GetUserAsync(User);
            var start = startDate ?? DateTime.Now.AddDays(-30);
            var end = endDate ?? DateTime.Now.AddDays(1);
            var treasury = await _treasuryService.GetOneAsync((int)user.TreasuryId, user.Id);
            // Tek satırda tüm veriyi alıyoruz!
            var model = await _treasuryService.GetDashboardDataAsync(user.Id,user.TreasuryId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(AddBankDTO model)
        {
            var user = await _userManager.GetUserAsync(User);

            // Modelden Entity'ye manuel map veya AutoMapper
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
            // Silme işlemi VehicleController'daki gibi ID üzerinden
            var result = _bankService.Delete(id);

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
            var result = _bankCardService.Delete(id);

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
        // 1. BANKA DETAYLARI
        public async Task<IActionResult> BankTransactions(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var bank = await _bankService.GetOneAsync(id, user.Id);
            if (bank == null) return RedirectToAction("Index");

            // Tüm işlemleri çek
            var transactions = await _transactionService.GetAllAsync(user.Id, (int)user.TreasuryId, x => x.BankId == id);

            // Son 30 günün tarihini belirle
            var lastMonth = DateTime.Now.AddDays(-30);

            var model = _mapper.Map<BankDetailsDTO>(bank);

            model.Transactions = transactions.OrderByDescending(x => x.TransactionDate).ToList();

            model.CurrentBalance = bank.Balance;
            model.TotalIncomingLastMonth = transactions
                 .Where(x => x.TransactionDate >= lastMonth && x.TransactionType == TransactionType.Incoming)
                 .Sum(x => x.Amount);

            model.TotalOutgoingLastMonth = transactions
                .Where(x => x.TransactionDate >= lastMonth && x.TransactionType == TransactionType.Outgoing)
                .Sum(x => x.Amount);



            return View("BankDetails", model);
        }

        public async Task<IActionResult> CardTransactions(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            // Kartı ve bağlı olduğu bankayı çekiyoruz
            var card = await _bankCardService.GetOneAsync(id, user.Id);

            if (card == null) return RedirectToAction("Index");

            // İşlem geçmişini çekiyoruz
            var transactions = await _transactionService.GetAllAsync(user.Id, (int)user.TreasuryId, x => x.BankCardId == id);

            // --- TARİH HESAPLAMA BAŞLANGIÇ ---
            var today = DateTime.Today;

            // 1. Hesap Kesim Tarihi
            int billingDay = card.BillingDay < 1 ? 1 : card.BillingDay;

            // Ayın kaç çektiğini kontrol et (Şubat 28 mi, Mart 31 mi?)
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            int safeBillingDay = Math.Min(billingDay, daysInMonth);

            var cutOffDate = new DateTime(today.Year, today.Month, safeBillingDay);

            
            if (today > cutOffDate)
            {
                var nextMonth = today.AddMonths(1);
                int daysInNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
                int safeBillingDayNext = Math.Min(billingDay, daysInNextMonth);
                cutOffDate = new DateTime(nextMonth.Year, nextMonth.Month, safeBillingDayNext);
            }

           
            int dueDay = card.DueDay < 1 ? 10 : card.DueDay; 

            var paymentDateTemp = new DateTime(cutOffDate.Year, cutOffDate.Month, 1);

           
            if (dueDay < billingDay)
            {
                paymentDateTemp = cutOffDate.AddMonths(1);
            }

            int daysInPaymentMonth = DateTime.DaysInMonth(paymentDateTemp.Year, paymentDateTemp.Month);
            int safeDueDay = Math.Min(dueDay, daysInPaymentMonth);

            var finalPaymentDate = new DateTime(paymentDateTemp.Year, paymentDateTemp.Month, safeDueDay);
            var daysLeft = (cutOffDate - today).Days;
            

          
            var model = _mapper.Map<CardDetailsDTO>(card);

            
            model.CutOffDay = cutOffDate;
            model.DaysLeftToCutOff = daysLeft;
            model.NextPaymentDate = finalPaymentDate;
            model.Transactions = transactions.OrderByDescending(x => x.TransactionDate).ToList();

            
            if (string.IsNullOrEmpty(model.BankName) && card.BankId != 0)
            {
                var bank = await _bankService.GetOneAsync(card.BankId, user.Id);
                if (bank != null) model.BankName = bank.BankName;
            }

            return View("CardDetails", model);
        }
    }
}