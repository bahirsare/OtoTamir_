using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.VehicleDTOs;

namespace OtoTamir.WEBUI.ViewComponents._Vehicle.ListVehicle
{
    public class _ListVehicleViewComponentPartial:ViewComponent
    { private readonly IVehicleService _vehicleService;

        public _ListVehicleViewComponentPartial(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedClientId)
        {
            var vehicles = await _vehicleService.GetAllAsync(v=> v.ClientId == selectedClientId);

            return View(vehicles);
        }
    }
}
