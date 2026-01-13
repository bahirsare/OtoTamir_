using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
using OtoTamir.BLL.Managers;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;
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
            var result =  await _bankCardService.DeleteAsync(id);

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
       
        public async Task<IActionResult> BankTransactions(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var bank = await _bankService.GetOneAsync(id, user.Id);
            if (bank == null) return RedirectToAction("Index");

            
            var transactions = await _transactionService.GetAllAsync(user.Id, (int)user.TreasuryId, x => x.BankId == id);

            
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

            
            var card = await _bankCardService.GetOneAsync(id, user.Id);

            if (card == null) return RedirectToAction("Index");

           
            var transactions = await _transactionService.GetAllAsync(user.Id, (int)user.TreasuryId, x => x.BankCardId == id);

           
            var today = DateTime.Today;

            
            int billingDay = card.BillingDay < 1 ? 1 : card.BillingDay;

            
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