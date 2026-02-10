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
        private readonly IClientService _clientService;
        private readonly UserManager<Mechanic> _userManager;

        public _ClientLastTransactionsViewComponentPartial(ITreasuryTransactionService transactionService, IClientService clientService, UserManager<Mechanic> userManager)
        {
            _clientService = clientService;
            _transactionService = transactionService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);

            var client = await _clientService.GetOneAsync(clientId, mechanic.Id, true, true);


            return View(client);
        }
    }
}
