using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
using OtoTamir.CORE.DTOs.VehicleDTOs;
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
        private readonly IMapper _mapper;



        public VehicleController(DataContext context, IVehicleService vehicleService, UserManager<Mechanic> userManager, IMapper mapper)
        {
            _context = context;
            _vehicleService = vehicleService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> VehicleDetails(int vehicleId)
        {
            var mechanic = await _userManager.GetUserAsync(User);
            if (mechanic == null)
            {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Account", "Login");
            }
            var vehicle = await _vehicleService.GetOneAsync(id: vehicleId, mechanicId: mechanic.Id, includeServiceRecords: true, includeClient: true);
            if (vehicle == null)
            {

                TempData["FailMessage"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Client");
            }
            return View(vehicle);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicleAsync(CreateVehicleDTO _model)
        {
            List<string> URL = _model.ReturnUrl.Split('/').ToList();
            if (!ModelState.IsValid)
            {

                TempData["FailMessage"] = "Araç Eklenemedi. Lütfen bilgileri eksiksiz doldurun.";
                if (URL[2]=="ClientDetails")
                    return RedirectToAction(URL[2], URL[1], new { clientId = 3 });
                return RedirectToAction(URL[2], URL[1]);
                
            }
            bool mustBeUnique = await _vehicleService
   .AnyAsync(v => v.Plate == _model.Plate && v.ClientId == _model.ClientId);

            if (!mustBeUnique)
            {

                var vehicle = _mapper.Map<Vehicle>(_model);

                var result = await _vehicleService.CreateAsync(vehicle);


                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Araç başarıyla eklendi.";
                }
                else
                {
                    TempData["FailMessage"] = "Araç eklenirken bir hata oluştu.";
                }
            }
                if (URL[2] == "ClientDetails")
                    return RedirectToAction(URL[2], URL[1], new { clientId = _model.ClientId });
                return RedirectToAction(URL[2], URL[1]);
            
            
        }
        public async Task<IActionResult> UpdateVehicle(EditVehicleDTO _model)
        {

            if (!ModelState.IsValid)
            {
                TempData["FailMessage"] = "Bilgileri kontrol edin.";
                return RedirectToAction("Details", new { id = _model.Id });
            }

            var mechanic = await _userManager.GetUserAsync(User);
            if (mechanic == null)
            {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Account", "Login");
            }
            var vehicle = await _vehicleService.GetOneAsync(id: _model.Id, mechanicId: mechanic.Id, includeServiceRecords: false, includeClient: false);
            if (vehicle == null)
            {
                TempData["FailMessage"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Client");
            }
            _mapper.Map(_model, vehicle);
            vehicle.Name = $"{vehicle.Plate}_{vehicle.Brand}".ToUpper();    
            vehicle.ModifiedDate=DateTime.Now;
            var result = await _vehicleService.UpdateAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Araç bilgileri başarıyla güncellendi.";
            }
            else
            {
                TempData["FailMessage"] = "Araç güncellenemedi.";
            }
            
            return RedirectToAction("VehicleDetails", new { vehicleId = _model.Id });

        }
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("VehicleDetails", "Vehicle", id);
            }
            var result = _vehicleService.Delete(id);
            if (result == 0)
            {
                TempData["FailMessage"] = "Araç silinemedi.";
                return RedirectToAction("VehicleDetails", "Vehicle", id);
            }
            TempData["SuccessMessage"] = "Araç silindi.";

            return RedirectToAction("Clients", "Client");
        }
    }
}
