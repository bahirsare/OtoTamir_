using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Client.ClientLastTransactions
{
    public class _ClientLastTransactionsViewComponentPartial : ViewComponent
    {
        private readonly ITreasuryTransactionService _transactionService;
        private readonly UserManager<Mechanic> _userManager;

        public _ClientLastTransactionsViewComponentPartial(ITreasuryTransactionService transactionService,UserManager<Mechanic> userManager)
        {
            _transactionService = transactionService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);

           

            var allTransactions = await _transactionService.GetAllAsync(mechanic.Id,mechanic.TreasuryId, filter: x => x.ClientId == clientId);

            var lastTransactions = allTransactions
                                    .OrderByDescending(x => x.TransactionDate)
                                    .Take(20)
                                    .ToList();

            ViewBag.ClientId = clientId;
            return View(lastTransactions);
        }
    }
}
