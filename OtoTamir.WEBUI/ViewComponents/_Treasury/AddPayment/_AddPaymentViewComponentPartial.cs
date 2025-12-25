using Microsoft.AspNetCore.Mvc;
using OtoTamir.WEBUI.Models;

namespace OtoTamir.WEBUI.ViewComponents._Treasury.AddPayment
{
    public class _AddPaymentViewComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke(int clientId, decimal currentBalance)
        {
            var model = new PaymentModalViewModel
            {
                ClientId = clientId,
                CurrentBalance = currentBalance
            };
            return View(model);
        }
    }
}
