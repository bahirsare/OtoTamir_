using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.Identity;

namespace OtoTamir.WEBUI.ViewComponents._Layout.Navbar
{
    public class _NavbarViewComponentPartial : ViewComponent
    {
        private readonly UserManager<Mechanic> _userManager;

        public _NavbarViewComponentPartial(UserManager<Mechanic> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(user); 
        }
    }
}
