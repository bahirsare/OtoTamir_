using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.DTOs.SymptomDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._ServiceRecord.EditServiceRecord
{
    public class _EditServiceRecordViewComponentPartial : ViewComponent
    {
        private readonly IServiceRecordService _serviceRecordService;
        private readonly IBankService _bankService;
        private readonly IPosTerminalService _posTerminalService;
        private readonly ISymptomService _symptomService;

        public _EditServiceRecordViewComponentPartial(
            IServiceRecordService serviceRecordService,
            IBankService bankService,
            IPosTerminalService posTerminalService,
            ISymptomService symptomService)
        {
            _serviceRecordService = serviceRecordService;
            _bankService = bankService;
            _posTerminalService = posTerminalService;
            _symptomService = symptomService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int recordId, string returnUrl)
        {
            var mechanicId = (HttpContext.User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // 1. İlgili servisi ve semptomlarını çek
            var record = await _serviceRecordService.GetOneAsync(recordId, mechanicId,true,true);

            // 2. İşlem yapılacak Semptomu bul (Örn: İlk 'Pending' olanı getiriyoruz)
            // Not: Birden fazla semptom varsa burada bir select listesi de yapılabilir ama senin senaryonda genelde tek ilerliyor gibi.
            var activeSymptom = record.SymptomList.FirstOrDefault(x => x.Status == SymptomStatus.Pending)
                                ?? record.SymptomList.FirstOrDefault();

            // 3. DTO'yu Hazırla (Senin metodunun beklediği model)
            var model = new ServiceWorkflowLogDTO
            {
                SymptomId = activeSymptom?.Id ?? 0,
                
                ReturnUrl = returnUrl ?? "/ServiceRecord/Ongoing",
                Status = activeSymptom?.Status ?? SymptomStatus.Pending,
                
                AdditionalDays = 0,
                AdditionalCost = 0
            };

           
            ViewBag.Banks = await _bankService.GetAllAsync(mechanicId);
            ViewBag.PosTerminals = await _posTerminalService.GetAllAsync(mechanicId);

            
            ViewBag.VehiclePlate = record.Vehicle.Plate;

            return View(model);
        }
    }
}
