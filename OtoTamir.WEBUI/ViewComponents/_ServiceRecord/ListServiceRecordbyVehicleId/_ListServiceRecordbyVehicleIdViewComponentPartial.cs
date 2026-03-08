using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Utilities;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._ServiceRecord.ListServiceRecord
{
    public class _ListServiceRecordbyVehicleIdViewComponentPartial : ViewComponent
    {
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<Mechanic> _userManager;

        public _ListServiceRecordbyVehicleIdViewComponentPartial(
            IVehicleService vehicleService,
            UserManager<Mechanic> userManager)
        {
            _vehicleService = vehicleService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int vehicleId, int page = 1)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var vehicle = await _vehicleService.GetOneAsync(
                id: vehicleId,
                mechanicId: mechanic.Id,
                includeServiceRecords: true,
                includeClient: true);

            if (vehicle == null)
                return View(vehicle);

            const int pageSize = 5; 

            var allRecords = vehicle.ServiceRecords
                .OrderByDescending(r => r.CreatedDate)
                .ToList();

            int rowCount = allRecords.Count;
            int pageCount = (int)Math.Ceiling((double)rowCount / pageSize);

            vehicle.ServiceRecords = allRecords
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["PagedResult"] = new PagedResultMeta
            {
                CurrentPage = page,
                PageCount = pageCount,
                PageSize = pageSize,
                RowCount = rowCount
            };
            ViewData["VehicleId"] = vehicleId;

            return View(vehicle);
        }
    }
}
