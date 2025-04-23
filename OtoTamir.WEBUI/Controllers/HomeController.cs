using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Migrations;
using OtoTamir.WEBUI.Models;

using System.Diagnostics;
using System.Net.Http.Headers;

namespace OtotamirWEBUI.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IClientService _clientService;
        private readonly IMechanicService _mechanicService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly IVehicleService _vehicleService;

        public HomeController(IClientService clientService, IMechanicService mechanicService,UserManager<Mechanic>  userManager, IVehicleService vehicleService)
        {
            _clientService = clientService;
            _mechanicService = mechanicService;
            _userManager = userManager;
            _vehicleService = vehicleService;
        }


       //[Authorize]
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Clients()

        {
            var mechanicId = _userManager.GetUserId(User);
            var clients = _clientService.GetAll(x => x.MechanicId == mechanicId);
            return View(clients);
        }
        [HttpPost]
        public  IActionResult CreateClient(CreateClientViewModel model)
        {
            

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Müþteri Eklenemedi, Lütfen Bilgileri Eksiksiz Doldurun";
                
                return RedirectToAction("Clients", "Home");
            }
            

            Client client = new Client()
            {
                Name = model.Name,
                Balance = model.Balance,
                PhoneNumber = model.PhoneNumber,//unique olmalý ilerde kontrol eklenecek
                Notes = model.Notes,
                MechanicId = _userManager.GetUserId(User)
            };

            var result = _clientService.Create(client);
            if (result == 1)
            {

                TempData["Message"] = "Müþteri Eklendi!";
            }
            else
            {
                TempData["Message"] = "Müþteri Eklenemedi.";


            }
            
            return RedirectToAction("Clients", "Home");
        }
        [HttpPost]
        public IActionResult CreateVehicle(CreateVehicleViewModel model)
        {
            if (!ModelState.IsValid) // model içine veri gelmiyor?
            {
                TempData["Message"] = "Araç Eklenemedi, Lütfen Bilgileri Eksiksiz Doldurun";

                return RedirectToAction("Clients", "Home");
            }
            Vehicle vehicle = new Vehicle()
            {
                Plate=model.Plate,
                Brand = model.Brand,
                Model = model.Model,
                Year = model.Year

            };
            
            var result =_vehicleService.Create(vehicle);
            if (result == 1)
            {
                TempData["Message"] = "Araç baþarýyla eklendi!";
            } else
            {
                TempData["Message"] = " Araç eklenemedi!";
            }
            return RedirectToAction("Clients", "Home"); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
