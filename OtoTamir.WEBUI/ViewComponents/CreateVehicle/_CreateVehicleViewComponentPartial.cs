using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.Entities;
using OtoTamir.WEBUI.Models;

namespace OtoTamir.WEBUI.ViewComponents.CreateVehicle
{
    public class _CreateVehicleViewComponentPartial: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int clientId) 
        {
            var model = new Vehicle();
            model.ClientId = clientId;
            return View(model);

        }

    }
}
