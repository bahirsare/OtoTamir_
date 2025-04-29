using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.Vehicle;

namespace OtoTamir.WEBUI.ViewComponents.Vehicle.CreateVehicle
{
    public class _CreateVehicleViewComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int clientId)
        {
            var model = new CreateVehicleDTO
            {
                ClientId = clientId
            };

            return View(model);
        }
    }
}
