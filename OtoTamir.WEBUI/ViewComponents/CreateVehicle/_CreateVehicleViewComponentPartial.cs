using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.Vehicle;

namespace OtoTamir.WEBUI.ViewComponents.CreateVehicle
{
    public class _CreateVehicleViewComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int clientId)
        {
            var model = new CreateVehicleDTO();
            model.ClientId = clientId;
            return View(model);

        }

    }
}
