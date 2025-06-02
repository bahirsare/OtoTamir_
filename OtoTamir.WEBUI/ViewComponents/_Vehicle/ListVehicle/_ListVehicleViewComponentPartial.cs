using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.VehicleDTOs;
using OtoTamir.CORE.Identity;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Vehicle.ListVehicle
{
    public class _ListVehicleViewComponentPartial:ViewComponent
    { private readonly IVehicleService _vehicleService;
      private readonly UserManager<Mechanic> _userManager;

        public _ListVehicleViewComponentPartial(IVehicleService vehicleService,UserManager<Mechanic> userManager)
        {
            _userManager=userManager;
            _vehicleService = vehicleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedClientId)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var vehicles = await _vehicleService.GetAllAsync(mechanic.Id,v=> v.ClientId == selectedClientId);

            return View(vehicles);
        }
    }
}
