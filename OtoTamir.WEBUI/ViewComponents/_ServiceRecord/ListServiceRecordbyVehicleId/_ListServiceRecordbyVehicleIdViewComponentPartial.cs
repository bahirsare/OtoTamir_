using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
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

        public async Task<IViewComponentResult> InvokeAsync(int vehicleId)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var vehicle= await _vehicleService.GetOneAsync(id: vehicleId, mechanicId:mechanic.Id,includeServiceRecords:true,includeClient:true);
            if (vehicle == null)
            {
                TempData["Messaage"] = "Araç bulunamadı.";
            }
            return View(vehicle);
        }
      

    }
}
