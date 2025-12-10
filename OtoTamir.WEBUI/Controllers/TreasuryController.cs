using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

namespace OtoTamir.WEBUI.Controllers
{
    [Authorize]
    public class TreasuryController : Controller
    {
        private readonly ITreasuryService _treasuryService;
        private readonly ITreasuryTransactionService _transactionService;
        private readonly IBankService _bankService;
        private readonly IBankCardService _bankCardService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly IMapper _mapper;


        public TreasuryController(
            ITreasuryService treasuryService,
            ITreasuryTransactionService transactionService,
            IBankService bankService,
            IBankCardService bankCardService,
            UserManager<Mechanic> userManager, IMapper mapper)
        {
            _treasuryService = treasuryService;
            _transactionService = transactionService;
            _bankService = bankService;
            _bankCardService = bankCardService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Kasa Kontrolü (Daha önce yazdığımız FixMissingTreasury mantığı burada da işe yarar)
            if (user.TreasuryId == null)
            {
                TempData["FailMessage"] = "Kasa bulunamadı. Lütfen önce kasanızı oluşturun.";
                return RedirectToAction("FixMissingTreasury", "Account");
            }

            var treasury = await _treasuryService.GetOneAsync((int)user.TreasuryId, user.Id);

            var model = new TreasuryDashboardViewModel
            {
                Treasury = treasury,
                // Listeleri dolduruyoruz
                Banks = await _bankService.GetAllAsync(user.Id),
                BankCards = await _bankCardService.GetAllAsync(user.Id),

                // Son işlemleri tarihe göre sıralı getir
                Transactions = (await _transactionService.GetAllAsync(user.Id, treasury.Id))
                                .OrderByDescending(t => t.TransactionDate)
                                .Take(50)
                                .ToList()
            };

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
           var result= await _bankCardService.CreateAsync(card);
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
    }
}