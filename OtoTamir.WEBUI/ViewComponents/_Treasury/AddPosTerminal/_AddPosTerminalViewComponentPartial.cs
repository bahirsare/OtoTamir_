using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract; 
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Treasury.AddPosTerminal
{
    public class _AddPosTerminalViewComponentPartial : ViewComponent
    {
        private readonly IBankService _bankService;

        public _AddPosTerminalViewComponentPartial(IBankService bankService)
        {
            _bankService = bankService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Kullanıcının bankalarını çekip Dropdown'a dolduracağız
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var banks = await _bankService.GetAllAsync(userId);

            ViewBag.Banks = banks; // View tarafında Select içinde kullanacağız

            return View(new AddPosTerminalDTO());
        }
    }
}
