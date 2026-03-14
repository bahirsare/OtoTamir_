using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OtoTamir.CORE.Identity;

namespace OtoTamir.WEBUI.Services.Filters
{
    public class SubscriptionCheckFilter : IAsyncActionFilter
    {
        private readonly UserManager<Mechanic> _userManager;

        public SubscriptionCheckFilter(UserManager<Mechanic> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // SADECE VERİ DEĞİŞTİREN (POST) İSTEKLERİ YAKALIYORUZ. 
            // GET (Sayfa görüntüleme) istekleri bu kalkana takılmaz, içeri girer.
            if (context.HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                var user = context.HttpContext.User;

                // Kullanıcı giriş yapmışsa ve "Admin" değilse (Admini engellemeyelim)
                if (user.Identity.IsAuthenticated && !user.IsInRole("Admin"))
                {
                    var mechanic = await _userManager.GetUserAsync(user);

                    // Ustanın süresi bitmiş mi kontrol et
                    if (mechanic != null && mechanic.SubscriptionEndDate < DateTime.Now)
                    {
                        // SÜRESİ BİTMİŞ! 
                        // İşlemi iptal et ve hata mesajıyla geldiği sayfaya (veya Anasayfaya) geri fırlat.
                        var controller = (Controller)context.Controller;
                        controller.TempData["ErrorMessage"] = "Lisans süreniz dolmuştur! Sistem şu an <b>Sadece Okunabilir (Read-Only)</b> moddadır. Yeni kayıt ekleyemez, silemez veya güncelleyemezsiniz.";

                        context.Result = new RedirectToActionResult("Index", "Home", null);
                        return; // İşlemi tamamen kes!
                    }
                }
            }

            // Süresi varsa veya işlem GET ise yola devam et
            await next();
        }
    }
}
