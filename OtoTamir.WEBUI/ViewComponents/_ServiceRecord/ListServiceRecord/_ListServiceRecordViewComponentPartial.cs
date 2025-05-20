using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.VehicleDTOs;
using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.ViewComponents._ServiceRecord.ListServiceRecord
{
    public class _ListServiceRecordViewComponentPartial : ViewComponent
    {
        private readonly IVehicleService _vehicleService;

        public _ListServiceRecordViewComponentPartial(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int vehicleid)
        {
            var vehicle= await _vehicleService.GetOneAsync(vehicleid);
            if (vehicle == null)
            {
                TempData["Messaage"] = "Araç bulunamadı.";
            }
            return View(vehicle);
        }

    }
}
