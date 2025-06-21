using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;

namespace OtoTamir.WEBUI.ViewComponents._Client._BalanceLog
{
    public class _BalanceLogViewComponentPartial: ViewComponent
    {
        private readonly IBalanceLogService _balanceLogService;
        private readonly UserManager<Mechanic> _userManager;
        public _BalanceLogViewComponentPartial(IBalanceLogService balanceLogService,  UserManager<Mechanic> userManager)
        {
            _balanceLogService = balanceLogService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId)
        {
            var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
            if (user == null) {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                return View();
            }
            var logs = await _balanceLogService.GetAllAsync(clientId:clientId,mechanicId:user.Id);
            return View(logs);
        }
    }
}
