using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.Client;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

using System.Diagnostics;

namespace OtotamirWEBUI.Controllers
{
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
        public IActionResult CreateClient(CreateClientDTO model)
        {


            if (!ModelState.IsValid)
            {
                TempData["Message"] = "M��teri Eklenemedi, L�tfen Bilgileri Eksiksiz Doldurun";

                return RedirectToAction("Clients", "Home");
            }


            Client client = _mapper.Map<Client>(model);
            client.MechanicId = _userManager.GetUserId(User);
            var result = _clientService.Create(client);
            if (result == 1)
            {

                TempData["Message"] = "M��teri Eklendi!";
            }
            else
            {
                TempData["Message"] = "M��teri Eklenemedi.";


            }

            return RedirectToAction("Clients", "Home");
        }
        [HttpPost]
        public IActionResult CreateVehicle(CreateVehicleDTO model)
        {
            //Console.WriteLine("ClientId: " + newVehicle.ClientId); // DEBUG
            if (!ModelState.IsValid) // model i�ine veri gelmiyor?
            {
                TempData["Message"] = "Ara� Eklenemedi, L�tfen Bilgileri Eksiksiz Doldurun";

                return RedirectToAction("Clients", "Home");
            }
            Vehicle vehicle= _mapper.Map<Vehicle>(model);
            var result = _vehicleService.Create(vehicle);
            if (result == 1)
            {
                TempData["Message"] = "Ara� ba�ar�yla eklendi!";
            }
            else
            {
                TempData["Message"] = " Ara� eklenemedi!";
            }
            return RedirectToAction("Clients", "Home");
        }
        public IActionResult DeleteClient(int id)
        {
            var client = _clientService.GetOne(id);
            if (client == null)
            {
                TempData["Message"] = "M��teri bulunamad�.";
                return RedirectToAction("Clients", "Home");
            }
            var result = _clientService.Delete(id);
            if (result > 0)
            {
                TempData["Message"] = "M��teri ba�ar�yla silindi.";
            }
            else
            {
                TempData["Message"] = "Bir hata olu�tu, m��teri silinemedi!";
            }
            return RedirectToAction("Clients", "Home");
        }
        [HttpPost]
        public IActionResult UpdateClient(CreateClientDTO model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "M��teri bilgileri ge�ersiz!";
                return RedirectToAction("Clients", "Home");
            }
            var client = _clientService.GetOne(model.Id);
            if (client == null)
            {
                TempData["Message"] = "M��teri bulunamad�.";
                return RedirectToAction("Clients", "Home");
            }
            client = _mapper.Map<Client>(model);
            var result = _clientService.Update();
            if (result > 0)
            {
                TempData["Message"] = "M��teri ba�ar�yla g�ncellendi.";
            }
            else
            {
                TempData["Message"] = "G�ncelleme s�ras�nda bir hata olu�tu.";
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
