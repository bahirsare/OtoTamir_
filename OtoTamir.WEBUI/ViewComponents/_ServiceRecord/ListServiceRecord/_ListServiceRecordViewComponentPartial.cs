using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs;
using OtoTamir.CORE.DTOs.VehicleDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._ServiceRecord.ListServiceRecord
{
    public class _ListServiceRecordViewComponentPartial : ViewComponent
    {
        private readonly IVehicleService _vehicleService;
        private readonly IServiceRecordService _serviceRecordService;
        private readonly UserManager<Mechanic> _userManager;

        public _ListServiceRecordViewComponentPartial(IVehicleService vehicleService, IServiceRecordService serviceRecordService, UserManager<Mechanic> userManager)
        {
            _vehicleService = vehicleService;
            _serviceRecordService = serviceRecordService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int vehicleid)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var vehicle= await _vehicleService.GetOneAsync(id:vehicleid,mechanicId:mechanic.Id,includeClient:true,includeServiceRecords:true);
            if (vehicle == null)
            {
                TempData["Messaage"] = "Araç bulunamadı.";
            }
            return View(vehicle);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var records= await _serviceRecordService.GetAllAsync(mechanicId:mechanic.Id, includeClient:true,includeSymptoms:true);
            if (records == null)
            {
                TempData["Messaage"] = "Kayıt bulunamadı.";
            }
            return View(records);
        }

    }
}
