using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.DTOs.VehicleDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

using System.Diagnostics;

namespace OtotamirWEBUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IClientService _clientService;
        private readonly IMechanicService _mechanicService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public HomeController(IClientService clientService, IMechanicService mechanicService, UserManager<Mechanic> userManager, IVehicleService vehicleService, IMapper mapper)
        {
            _clientService = clientService;
            _mechanicService = mechanicService;
            _userManager = userManager;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> Clients()

        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.IsProfileCompleted)
            {
                TempData["Message"] = "Lütfen bilgilerinizi doldurunuz";
                return RedirectToAction("Profile", "Account");
            }

            var clients = await _clientService.GetAllAsync(user.Id, includeVehicles: true, includeServiceRecords: false);
            return View(clients);
        }
        

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
