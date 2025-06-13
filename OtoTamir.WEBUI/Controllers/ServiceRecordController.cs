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
using OtoTamir.WEBUI.Services;
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

    
    [HttpGet]
    public async Task<IActionResult> Ongoing(ListServiceRecordsDTO model)
    {
        var mechanic = await _userManager.GetUserAsync(User);

        Expression<Func<ServiceRecord, bool>> filter = sr => true;

        if (!string.IsNullOrWhiteSpace(model.CurrentStatus))
        {
            filter = filter.AndAlso(sr => sr.Status == model.CurrentStatus);
        }

        if (model.StartDate.HasValue)
        {
            filter = filter.AndAlso(sr => sr.CreatedDate >= model.StartDate.Value);
        }

        if (model.EndDate.HasValue)
        {
            filter = filter.AndAlso(sr => sr.CreatedDate <= model.EndDate.Value);
        }

        var records = await _serviceRecordService.GetAllAsync(
            mechanic.Id,
            includeVehicle: true,
            includeClient: true,
            includeSymptoms: false,
            filter: filter
        );

        
        if (!string.IsNullOrWhiteSpace(model.ClientName))
        {
            records = records.Where(r =>
                r.Vehicle.Client.Name.Contains(model.ClientName, StringComparison.OrdinalIgnoreCase)).ToList();
        } 
        if (!string.IsNullOrWhiteSpace(model.VehicleName))
        {
            records = records.Where(r =>
                r.Vehicle.Name.Contains(model.VehicleName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Sıralama
        if (!string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection))
        {
            records = model.SortColumn switch
            {
                "CreatedDate" => model.SortDirection == "asc"
                    ? records.OrderBy(r => r.CreatedDate).ToList()
                    : records.OrderByDescending(r => r.CreatedDate).ToList(),

                "Status" => model.SortDirection == "asc"
                    ? records.OrderBy(r => r.Status).ToList()
                    : records.OrderByDescending(r => r.Status).ToList(),

                "ClientName" => model.SortDirection == "asc"
                    ? records.OrderBy(r => r.Vehicle.Client.Name).ToList()
                    : records.OrderByDescending(r => r.Vehicle.Client.Name).ToList(),

                _ => records.OrderByDescending(r => r.CreatedDate).ToList()
            };
        }
        else
        {
            records = records.OrderByDescending(r => r.CreatedDate).ToList(); // Varsayılan sıralama
        }

        model.Records = records;

        return View(model);
    }

   

    [HttpPost]
    public async Task<IActionResult> BulkComplete(List<int> ids)
    {
        if (ids == null || !ids.Any())
        {
            TempData["Error"] = "Hiçbir kayıt seçilmedi.";
            return RedirectToAction("Ongoing");
        }
        var mechanic= await _userManager.GetUserAsync(User);
        foreach (int item in ids) {
            var record = await _serviceRecordService.GetOneAsync(item, mechanic.Id, false, false);
            record.Status = "Tamamlandı";
            record.CompletedDate = DateTime.Now;
            await _serviceRecordService.UpdateAsync();
        }
        

        TempData["Success"] = $"{ids.Count} kayıt tamamlandı olarak işaretlendi.";
        return RedirectToAction("Ongoing");
    }


}



