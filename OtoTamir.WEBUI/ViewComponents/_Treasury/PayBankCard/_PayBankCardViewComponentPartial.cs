using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Treasury.PayBankCard
{
    public class _PayBankCardViewComponentPartial : ViewComponent
    {
        private readonly IBankService _bankService; // veya IBankDal
        // Kullanıcı bilgisini almak için HttpContextAccessor gerekebilir ama
        // ViewComponent içinde User.Identity'ye erişebiliriz.

        public _PayBankCardViewComponentPartial(IBankService bankService)
        {
            _bankService = bankService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // O anki kullanıcının (Tamircinin) ID'sini al
            var userIdStr = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          

            var banks = new List<Bank>();
            if (!string.IsNullOrEmpty(userIdStr))
            {
                banks = await _bankService.GetAllAsync(userIdStr);
            }

            return View(banks);
        }
    }
}
