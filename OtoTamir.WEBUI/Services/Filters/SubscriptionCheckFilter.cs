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
            
            if (context.HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                var user = context.HttpContext.User;

              
                if (user.Identity.IsAuthenticated && !user.IsInRole("Admin"))
                {
                    var mechanic = await _userManager.GetUserAsync(user);

                    if (mechanic != null && mechanic.SubscriptionEndDate < DateTime.Now)
                    {
                      
                        var controller = (Controller)context.Controller;
                        controller.TempData["ErrorMessage"] = "Lisans süreniz dolmuştur! Sistem şu an <b>Sadece Okunabilir (Read-Only)</b> moddadır. Yeni kayıt ekleyemez, silemez veya güncelleyemezsiniz.";

                        context.Result = new RedirectToActionResult("Index", "Home", null);
                        return; 
                    }
                }
            }

            await next();
        }
    }
}
