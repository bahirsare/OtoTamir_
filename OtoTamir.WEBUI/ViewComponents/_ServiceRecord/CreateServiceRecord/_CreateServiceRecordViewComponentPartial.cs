﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.SymptomDTOs;
using OtoTamir.CORE.Identity;

namespace OtoTamir.WEBUI.ViewComponents._ServiceRecord.CreateServiceRecord
{
    public class _CreateServiceRecordViewComponentPartial : ViewComponent
    {
        public readonly IVehicleService _vehicleService;
        public readonly UserManager<Mechanic> _userService;

        public _CreateServiceRecordViewComponentPartial(IVehicleService vehicleService, UserManager<Mechanic> userService)
        {
            _vehicleService = vehicleService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int vehicleId, string returnContext)
        {
            var mechanic = await _userService.GetUserAsync(UserClaimsPrincipal);
            var vehicle = await _vehicleService.GetOneAsync(id: vehicleId, mechanicId: mechanic.Id);

            string returnController;
            string returnAction;
            int? returnId = null;

            if (returnContext == "ServiceRecord")
            {
                returnController = "ServiceRecord";
                returnAction = "GetClientDetails";
                returnId = vehicle.ClientId;
            }
            else if (returnContext == "Vehicle")
            {
                returnController = "Vehicle";
                returnAction = "VehicleDetails";
                returnId = vehicle.Id;
            }
            else
            {
                returnController = "Home";
                returnAction = "Index";
            }

            var model = new CreateSymptomGroupDTO()
            {
                VehicleId = vehicleId,
                ReturnController = returnController,
                ReturnAction = returnAction,
                ReturnId = returnId
            };

            return View(model);
        }

    }
}
