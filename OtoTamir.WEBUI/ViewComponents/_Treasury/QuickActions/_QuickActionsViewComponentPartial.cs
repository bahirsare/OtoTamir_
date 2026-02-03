using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;

namespace OtoTamir.WEBUI.ViewComponents._Treasury.AddExpense
{
    public class _QuickActionsViewComponentPartial : ViewComponent
    {
        private readonly IBankService _bankService;
        private readonly IBankCardService _bankCardService;
        private readonly UserManager<Mechanic> _userManager;

        public _QuickActionsViewComponentPartial(IBankService bankService, IBankCardService bankCardService, UserManager<Mechanic> userManager)
        {
            _bankService = bankService;
            _bankCardService = bankCardService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.Banks = await _bankService.GetAllAsync(user.Id);
            ViewBag.BankCards = await _bankCardService.GetAllAsync(user.Id);
            ViewBag.Categories = new List<TransactionCategory>();

            return View();
        }
    }
}
