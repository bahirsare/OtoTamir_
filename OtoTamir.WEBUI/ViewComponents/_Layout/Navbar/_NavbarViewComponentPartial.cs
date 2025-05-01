using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;

namespace OtoTamir.WEBUI.ViewComponents._Layout.Navbar
{
    public class _NavbarViewComponentPartial : ViewComponent
    {
        private readonly UserManager<Mechanic> _userManager;
        private readonly IMechanicService _mechanicService;

        public _NavbarViewComponentPartial(UserManager<Mechanic> userManager, IMechanicService mechanicService)
        {
            _userManager = userManager;
            _mechanicService = mechanicService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var mechanic = _mechanicService.GetOne(user.Id);
            if (mechanic.Image == null)
            {
                Image i =new Image()
                {
                    Url = "avatar.png"
                };
                mechanic.Image = i;
            }

            return View(mechanic);
        }
    }
}
