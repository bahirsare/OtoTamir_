using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.Client;
using OtoTamir.CORE.Entities;
using OtoTamir.WEBUI.Models;

namespace OtoTamir.WEBUI.ViewComponents.CreateVehicle
{
    public class _CreateVehicleViewComponentPartial: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int clientId) 
        {
            var model = new CreateVehicleDTO();
            model.ClientId = clientId;
            return View(model);

        }

    }
}
