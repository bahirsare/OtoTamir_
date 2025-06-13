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
    public class _ListServiceRecordbyVehicleIdViewComponentPartial : ViewComponent
    {
        private readonly IVehicleService _vehicleService;
        private readonly IServiceRecordService _serviceRecordService;
        private readonly UserManager<Mechanic> _userManager;

        public _ListServiceRecordbyVehicleIdViewComponentPartial(IVehicleService vehicleService, IServiceRecordService serviceRecordService, UserManager<Mechanic> userManager)
        {
            _vehicleService = vehicleService;
            _serviceRecordService = serviceRecordService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int recordId)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var record= await _serviceRecordService.GetOneAsync(id: recordId, mechanicId:mechanic.Id,includeVehicle:true,includeSymptoms:true);
            if (record == null)
            {
                TempData["Messaage"] = "Kayıt bulunamadı.";
            }
            return View(record);
        }
      

    }
}
