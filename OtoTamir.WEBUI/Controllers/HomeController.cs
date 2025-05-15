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


        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> ClientsAsync()

        {
            var mechanicId = _userManager.GetUserId(User);
            var clients =await _clientService.GetAllAsync(x => x.MechanicId == mechanicId);
            return View(clients);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDTO model)
        {


            if (!ModelState.IsValid)
            {
                TempData["Message"] = "M��teri Eklenemedi, L�tfen Bilgileri Eksiksiz Doldurun";

                return RedirectToAction("Clients", "Home");
            }
            var currentMechanicId = _userManager.GetUserId(User);

            bool mustBeUnique = await !_clientService
    .GetAllAsync(i => i.PhoneNumber == model.PhoneNumber && i.MechanicId == currentMechanicId)
    .Any();


            if (mustBeUnique )
            {

                Client client = _mapper.Map<Client>(model);
                client.MechanicId = _userManager.GetUserId(User);
                var result =await _clientService.CreateAsync(client);
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
            else
            {
                TempData["FailMessage"] = "Ayn� telefon numaras�na sahip m��teri var!";
                return RedirectToAction("Clients", "Home"); ;
            }
        }

        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var client = await _clientService.GetOneAsync(id);
            if (client == null)
            {
                TempData["FailMessage"] = "M��teri bulunamad�.";
                return RedirectToAction("Clients", "Home");
            }
            var result =await _clientService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateClientAsync(EditClientDTO model, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "M��teri bilgileri ge�ersiz!";
                return RedirectToAction("Clients", "Home");
            }
            var client =await _clientService.GetOneAsync(model.Id);
            if (client == null)
            {
                TempData["Message"] = "M��teri bulunamad�.";
                return RedirectToAction("Clients", "Home");
            }
            _mapper.Map(model, client);
            var result = await _clientService.UpdateAsync();
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
        [HttpPost]
        public async Task<IActionResult> CreateVehicleAsync(CreateVehicleDTO _model)
        {
            

            if (!ModelState.IsValid)
            {
               
                TempData["Message"] = "Ara� Eklenemedi. L�tfen bilgileri eksiksiz doldurun.";
                return RedirectToAction("Clients", "Home");
            }
            _model.Plate.ToUpper().Replace(" ","");

            var vehicle = _mapper.Map<Vehicle>(_model);

            var result = await _vehicleService.CreateAsync(vehicle);

            if (result > 0)
            {
                TempData["Message"] = "Ara� ba�ar�yla eklendi.";
            }
            else
            {
                TempData["Message"] = "Ara� eklenirken bir hata olu�tu.";
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
