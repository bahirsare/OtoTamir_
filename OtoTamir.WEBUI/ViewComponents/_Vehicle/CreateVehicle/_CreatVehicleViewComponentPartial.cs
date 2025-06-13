using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.VehicleDTOs;

namespace OtoTamir.WEBUI.ViewComponents._Vehicle.CreateVehicle
{
    public class _CreateVehicleViewComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CreateVehicleDTO model) 
        {
            model.Year=DateTime.Now.Year;
            return View(model);
        }
    }
}
