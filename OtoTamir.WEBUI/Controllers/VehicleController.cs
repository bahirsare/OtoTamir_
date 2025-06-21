using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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



        public VehicleController(DataContext context, IVehicleService vehicleService, UserManager<Mechanic> userManager,IMapper mapper)
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
        [HttpPost]
        public async Task<IActionResult> CreateVehicleAsync(CreateVehicleDTO _model)
        {
            List<string> URL = _model.ReturnUrl.Split('/').ToList();
            if (!ModelState.IsValid)
            {

                TempData["Message"] = "Araç Eklenemedi. Lütfen bilgileri eksiksiz doldurun.";
                return RedirectToAction(URL[2], URL[1]);
            }


            var vehicle = _mapper.Map<Vehicle>(_model);

            var result = await _vehicleService.CreateAsync(vehicle);

            if (result > 0)
            {
                TempData["Message"] = "Araç başarıyla eklendi.";
            }
            else
            {
                TempData["Message"] = "Araç eklenirken bir hata oluştu.";
            }

            return RedirectToAction(URL[2], URL[1]);
        }
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("VehicleDetails", "Vehicle",id);
            }
            var result =  _vehicleService.Delete(id);
            if (result == 0) {
                TempData["FailMessage"] = "Araç silinemedi.";
                return RedirectToAction("VehicleDetails", "Vehicle", id);
            }
            TempData["SuccessMessage"] = "Araç silindi.";

            return RedirectToAction("Clients", "Home");
        }
    }
}
