

using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.MechanicDTOs;
using OtoTamir.WEBUI.Models;

namespace OtoTamir.WEBUI.ViewComponents._Mechanic.ChangePassword
{
    public class _ChangePasswordViewComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = new ChangePasswordViewModel();

            return View(model);

        }
    }
}
