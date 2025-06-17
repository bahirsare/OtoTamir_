using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System.Security.Cryptography.X509Certificates;

namespace OtoTamir.WEBUI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMechanicService _mechanicService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMechanicService mechanicService, UserManager<Mechanic> userManager, IVehicleService vehicleService, IMapper mapper)
        {
            _clientService = clientService;
            _userManager = userManager;           
            _mapper = mapper;
        }
        public async Task<IActionResult> ClientDetails(int clientId)
        {

            var mechanic = await _userManager.GetUserAsync(User);
            if (mechanic == null) {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                RedirectToAction("Clients", "Home");
            }
            var client = await _clientService.GetOneAsync(clientId, mechanic.Id, includeServiceRecords: true, includeVehicles: true);
            if (client == null) {

                TempData["FailMessage"] = "Müşteri bulunamadı.";
                RedirectToAction("Clients", "Home");
            }
            return View(client);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDTO model)
        {
            List<string> URL = model.ReturnUrl.Split('/').ToList();
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Müşteri Eklenemedi, Lütfen Bilgileri Eksiksiz Doldurun";

                return RedirectToAction(URL[0], URL[1]);
            }
            var currentMechanicId = _userManager.GetUserId(User);

            bool mustBeUnique = await _clientService
    .AnyAsync(i => i.PhoneNumber == model.PhoneNumber && i.MechanicId == currentMechanicId);

            if (!mustBeUnique)
            {
                model.PhoneNumber = model.PhoneNumber?.Replace(" ", "");
                Client client = _mapper.Map<Client>(model);
                client.MechanicId = _userManager.GetUserId(User);
                var result = await _clientService.CreateAsync(client);
                if (result == 1)
                {
                    TempData["Message"] = "Müşteri Eklendi!";
                }
                else
                {
                    TempData["Message"] = "Müşteri Eklenemedi.";
                }
                return RedirectToAction(URL[0], URL[1]);
            }
            else
            {
                TempData["FailMessage"] = "Aynı telefon numarasına sahip müşteri var!";
                return RedirectToAction(URL[0], URL[1]);
            }
        }
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var mechanicId = _userManager.GetUserId(User);
            var client = await _clientService.GetOneAsync(id, mechanicId, includeVehicles: false, includeServiceRecords: false);
            if (client == null)
            {
                TempData["FailMessage"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Home");
            }
            var result = await _clientService.DeleteAsync(id);
            if (result > 0)
            {
                TempData["Message"] = "Müşteri başarıyla silindi.";
            }
            else
            {
                TempData["Message"] = "Bir hata oluştu, müşteri silinemedi!";
            }
            return RedirectToAction("Clients", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClientAsync(EditClientDTO model, IFormFile image)
        {
            var mechanicId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Müşteri bilgileri geçersiz!";
                return RedirectToAction("Clients", "Home");
            }
            var client = await _clientService.GetOneAsync(model.Id, mechanicId, includeVehicles: false, includeServiceRecords: false);
            if (client == null)
            {
                TempData["Message"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Home");
            }
            _mapper.Map(model, client);
            var result = await _clientService.UpdateAsync();
            if (result > 0)
            {
                TempData["Message"] = "Müşteri başarıyla güncellendi.";
            }
            else
            {
                TempData["Message"] = "Güncelleme sırasında bir hata oluştu.";
            }
            return RedirectToAction("Clients", "Home");

        }
    }
}
