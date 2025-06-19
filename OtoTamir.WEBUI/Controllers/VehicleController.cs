using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Context;

namespace OtoTamir.WEBUI.Controllers
{
    public class VehicleController : Controller
    {
        private readonly DataContext _context;
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<Mechanic> _userManager;

             
        
        public VehicleController(DataContext context, IVehicleService vehicleService, UserManager<Mechanic> userManager)
        { 
            _context = context;
            _vehicleService = vehicleService;
            _userManager = userManager;
        }
        public async Task<IActionResult> VehicleDetails(int vehicleId)
        {
            var mechanic = await _userManager.GetUserAsync(User);
            if (mechanic == null)
            {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Clients", "Home");
            }
            var vehicle = await _vehicleService.GetOneAsync(id:vehicleId,mechanicId: mechanic.Id, includeServiceRecords: true,includeClient:true);
            if (vehicle == null)
            {

                TempData["FailMessage"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Home");
            }
            return View(vehicle);
        }
    }
}
