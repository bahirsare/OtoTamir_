using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.DTOs.SymptomDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System.Linq.Expressions;
[Authorize]
public class ServiceRecordController : Controller
{
    private readonly IVehicleService _vehicleService;
    private readonly IClientService _clientService;
    private readonly IServiceRecordService _serviceRecordService;
    private readonly ISymptomService _symptomService;
    private readonly IMapper _mapper;
    private readonly UserManager<Mechanic> _userManager;

    public ServiceRecordController(IVehicleService vehicleService, IClientService clientService, IServiceRecordService serviceRecordService, ISymptomService symptomService, IMapper mapper, UserManager<Mechanic> userManager)
    {
        _vehicleService = vehicleService;
        _clientService = clientService;
        _serviceRecordService = serviceRecordService;
        _symptomService = symptomService;
        _mapper = mapper;
        _userManager = userManager;
    }


    public async Task<IActionResult> Index(int? selectedClientId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (!user.IsProfileCompleted)
        {
            TempData["Message"] = "Lütfen bilgilerinizi doldurunuz";
            return RedirectToAction("Profile", "Account");
        }
        var clients = await _clientService.GetAllAsync(user.Id, false, false);
        var model = new ListClientDTO
        {
            Clients = clients,

        };
        if (selectedClientId != null)
        {
            model.SelectedClientId = (int)selectedClientId;
        }
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> GetVehiclesByClientId(int selectedClientId)
    {
        var user = await _userManager.GetUserAsync(User);
        var clients = await _clientService.GetAllAsync(user.Id, true, false);
        var model = new ListClientDTO
        {
            Clients = clients,
            SelectedClientId = selectedClientId
        };
        if (clients.Count == 0)
        {
            clients = await _clientService.GetAllAsync(user.Id, false, false);
        }
        return View("Index", model);
    }
    [HttpPost]
    public async Task<IActionResult> CreateServiceRecord(CreateSymptomGroupDTO model)
    {
        var user = await _userManager.GetUserAsync(User);
        var vehicle = await _vehicleService.GetOneAsync(id: model.VehicleId, mechanicId: user.Id, includeClient: true, includeServiceRecords: true);
        if (vehicle == null)
        {
            TempData["FailMessage"] = "Araç bulunamadı!";
            return RedirectToAction("Index");
        }

        int clientId = vehicle.ClientId;

        if (!ModelState.IsValid)
        {
            TempData["FailMessage"] = "Girilen bilgiler geçersiz veya eksik.";
            return RedirectToAction("Index", new { selectedClientId = clientId });
        }

        var totalCost = model.Symptoms.Sum(s => s.EstimatedCost);
        var serviceRecord = new ServiceRecord
        {
            VehicleId = model.VehicleId,
            Name = $"{DateTime.Now:yyyy-MM-dd HH:mm} Tarihli Servis Kaydı",
            Description = string.Join(" , ", model.Symptoms.Select(s => s.Name + ": " + s.Description)),
            Price = totalCost,
            Status = "Devam Ediyor",
            AuthorName = model.AuthorName

        };
        var result = await _serviceRecordService.CreateAsync(serviceRecord);
        if (result == 0)
        {
            TempData["FailMessage"] = "Servis kaydı oluşturulamadı!";
            return RedirectToAction("Index", new { selectedClientId = clientId });
        }
        TempData["SuccessMessage"] = "Servis kaydı oluşturuldu!";

        foreach (var item in model.Symptoms)
        {
            var symptom = _mapper.Map<Symptom>(item);
            symptom.ServiceRecordId = serviceRecord.Id;

            var symptomResult = await _symptomService.CreateAsync(symptom);

        }
        return RedirectToAction("Index", new { selectedClientId = clientId });
    }

    public async Task<IActionResult> Ongoing(ListServiceRecordsDTO model)
    {
        var mechanicId = _userManager.GetUserId(User);


        Expression<Func<ServiceRecord, bool>> filter = sr =>
        sr.Vehicle.Client.MechanicId == mechanicId &&
        (string.IsNullOrEmpty(model.ClientName) || sr.Vehicle.Client.Name.Contains(model.ClientName)) &&
        (string.IsNullOrEmpty(model.CurrentStatus) || sr.Status == model.CurrentStatus);

        
        model.Records = await _serviceRecordService.GetAllAsync(
            mechanicId,
            filter: filter,
            includeVehicle: true,
            includeClient: true,
            includeSymptoms: true);
        return View(model);
    }

    //public async Task<IActionResult> 
    //[HttpGet]
    //public async Task<IActionResult> OngoingPartial(FilterModel filter, int page = 1)
    //{
    //    var result = await _serviceRecordService.GetPagedRecordsAsync(filter, page);
    //    return Json(new
    //    {
    //        table = await this.RenderViewAsync("_RecordsTablePartial", result.Records, true),
    //        pagination = await this.RenderViewAsync("_PaginationPartial", result.Pagination, true)
    //    });
    //}

    //[HttpGet]
    //public async Task<IActionResult> GetRecord(int id)
    //{
    //    var record = await _serviceRecordService.GetRecordAsync(id);
    //    return Json(record);
    //}

    //[HttpPost]
    //public async Task<IActionResult> BulkComplete([FromBody] int[] ids)
    //{
    //    await _serviceRecordService.BulkCompleteAsync(ids);
    //    return Ok();
    //}

    //public async Task<IActionResult> ExportExcel(FilterModel filter)
    //{
    //    var records = await _serviceRecordService.GetRecordsForExportAsync(filter);
    //    // Excel export işlemleri
    //    return File(excelBytes, "application/vnd.ms-excel", "ServisKayitlari.xlsx");
    //}


}



