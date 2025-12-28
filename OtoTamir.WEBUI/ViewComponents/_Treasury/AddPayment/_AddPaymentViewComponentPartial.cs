using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.WEBUI.Models;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Treasury.AddPayment
{
    public class _AddPaymentViewComponentPartial: ViewComponent
    {
        private readonly IBankService _bankService;
        private readonly IPosTerminalService _posTerminalService;
        public _AddPaymentViewComponentPartial(IBankService bankService, IPosTerminalService posTerminalService)
        {
            _bankService = bankService;
            _posTerminalService = posTerminalService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int clientId, decimal currentBalance)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ViewBag.Banks = await _bankService.GetAllAsync(userId);
            ViewBag.PosTerminals = await _posTerminalService.GetAllAsync(userId);

            var model = new PaymentModalViewModel
            {
                ClientId = clientId,
                CurrentBalance = currentBalance
            };
            return View(model);
        }
    }
}
