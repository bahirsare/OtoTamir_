using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Client.CardClient
{
    public class _CardClientViewComponentPartial:ViewComponent
    {
        private readonly IClientService _clientService;
        private readonly UserManager<Mechanic> _userManager;

        public _CardClientViewComponentPartial(IClientService clientService, UserManager<Mechanic> userManager)
        {
            _clientService = clientService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId,string returnUrl)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var model = await _clientService.GetOneAsync(clientId,mechanic.Id,includeVehicles:true,includeServiceRecords:false);
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }
    }
}
