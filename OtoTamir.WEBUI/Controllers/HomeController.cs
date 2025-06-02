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
                TempData["Message"] = "L�tfen bilgilerinizi doldurunuz";
                return RedirectToAction("Profile", "Account");
            }
            
            var clients = await _clientService.GetAllAsync(user.Id,includeVehicles:true,includeServiceRecords:false);
            return View(clients);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDTO model)
        {
            List<string>URL= model.ReturnUrl.Split('/').ToList();
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "M��teri Eklenemedi, L�tfen Bilgileri Eksiksiz Doldurun";

                return RedirectToAction(URL[0], URL[1]);
            }
            var currentMechanicId = _userManager.GetUserId(User);

            bool mustBeUnique = await _clientService
    .AnyAsync(i => i.PhoneNumber == model.PhoneNumber && i.MechanicId == currentMechanicId);

            if (!mustBeUnique)
            {
                Client client = _mapper.Map<Client>(model);
                client.MechanicId = _userManager.GetUserId(User);
                var result = await _clientService.CreateAsync(client);
                if (result == 1)
                {
                    TempData["Message"] = "M��teri Eklendi!";
                }
                else
                {
                    TempData["Message"] = "M��teri Eklenemedi.";
                }
                return RedirectToAction(URL[0], URL[1]);
            }
            else
            {
                TempData["FailMessage"] = "Ayn� telefon numaras�na sahip m��teri var!";
                return RedirectToAction(URL[0], URL[1]);
            }
        }
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var mechanicId = _userManager.GetUserId(User);
            var client = await _clientService.GetOneAsync(id,mechanicId,includeVehicles:false,includeServiceRecords:false);
            if (client == null)
            {
                TempData["FailMessage"] = "M��teri bulunamad�.";
                return RedirectToAction("Clients", "Home");
            }
            var result = await _clientService.DeleteAsync(id);
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
            var mechanicId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "M��teri bilgileri ge�ersiz!";
                return RedirectToAction("Clients", "Home");
            }
            var client = await _clientService.GetOneAsync(model.Id,mechanicId,includeVehicles:false,includeServiceRecords:false);
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
            List<string> URL = _model.ReturnUrl.Split('/').ToList();
            if (!ModelState.IsValid)
            {

                TempData["Message"] = "Ara� Eklenemedi. L�tfen bilgileri eksiksiz doldurun.";
                return RedirectToAction(URL[1], URL[0]);
            }
            _model.Plate.ToUpper().Replace(" ", "");

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

            return RedirectToAction(URL[1],URL[0] );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
