using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        if (selectedClientId == null)
        {
            TempData["FailMessage"] = "Görüntülemek istediğiniz müşteriyi seçiniz.";
            return View("Index");
        }
        var user = await _userManager.GetUserAsync(User);
        var clients = await _clientService.GetAllAsync(user.Id, true, false, i => i.Id != selectedClientId);
        var model = new ListClientDTO
        {
            Clients = clients,
            SelectedClientId = selectedClientId,

        };
        model.SelectedClientName = (await _clientService.GetOneAsync((int)model.SelectedClientId, user.Id, false, false)).Name;
        if (clients.Count == 0)
        {
            clients = await _clientService.GetAllAsync(user.Id, false, false);
        }
        return View("Index", model);
    }
    [HttpGet]
    public async Task<IActionResult> GetClientDetails(int clientId)
    {
        var user = await _userManager.GetUserAsync(User);
        var clients = await _clientService.GetAllAsync(user.Id, true, false, i => i.Id != clientId);
        var model = new ListClientDTO
        {
            Clients = clients,
            SelectedClientId = clientId,

        };
        model.SelectedClientName = (await _clientService.GetOneAsync((int)model.SelectedClientId, user.Id, false, false)).Name;
        if (clients.Count == 0)
        {
            clients = await _clientService.GetAllAsync(user.Id, false, false);
        }
        return View("Index", model);

    }
    [HttpPost]
    public async Task<IActionResult> AddServiceWorkflowLogs(ServiceWorkflowLogDTO WorkflowLogDTO)//status bilgisi işlenmedi daha
    {
        if (!ModelState.IsValid)
        {
            TempData["FailMessage"] = "Lütfen kaydı eksiksiz doldurun.";
            return RedirectToAction("ongoing", "ServiceRecord");

        }
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["FailMessage"] = "Bu işlemi yapmak için giriş yapmanız gerekmektedir.";
            return RedirectToAction("Login", "Account");
        }
        var symptom = await _symptomService.GetOneAsync(mechanicId: user.Id, id: WorkflowLogDTO.SymptomId);
        if (symptom == null)
        {
            TempData["FailMessage"] = "Semptom bulunamadı.";
            return RedirectToAction("ongoing", "ServiceRecord");
        }

        var WorkflowLog = _mapper.Map<RepairComment>(WorkflowLogDTO);
        WorkflowLog.ModifiedDate = DateTime.Now;
        WorkflowLog.CreatedDate = DateTime.Now;
        symptom.ServiceWorkflowLogs.Add(WorkflowLog);
        var result = await _symptomService.UpdateAsync();
        if (result == 0)
        {
            TempData["FailMessage"] = "Log kaydı eklenemedi.";
            return RedirectToAction("ongoing", "ServiceRecord");
        }
        TempData["SuccessMessage"] = "Log kaydı eklendi.";
        return RedirectToAction("ongoing", "ServiceRecord");
    }

    [HttpPost]
    public async Task<IActionResult> CreateServiceRecord(CreateSymptomGroupDTO model)
    {
        var user = await _userManager.GetUserAsync(User);
        var vehicle = await _vehicleService.GetOneAsync(id: model.VehicleId, mechanicId: user.Id, includeClient: true, includeServiceRecords: true);

        if (vehicle == null)
        {
            TempData["FailMessage"] = "Araç bulunamadı!";
            return RedirectToAction(model.ReturnAction, model.ReturnController, new { id = model.ReturnId });
        }

        if (!ModelState.IsValid)
        {
            TempData["FailMessage"] = "Girilen bilgiler geçersiz veya eksik.";
            return RedirectToAction(model.ReturnAction, model.ReturnController, new { id = model.ReturnId });
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
            return RedirectToAction(model.ReturnAction, model.ReturnController, new { id = model.ReturnId });
        }

        foreach (var item in model.Symptoms)
        {
            var symptom = _mapper.Map<Symptom>(item);
            symptom.ServiceRecordId = serviceRecord.Id;

            var symptomResult = await _symptomService.CreateAsync(symptom);
            if (symptomResult == 0)
            {
                TempData["FailMessage"] = "Semptom oluşturulamadı!";
                break;
            }
        }

        TempData["SuccessMessage"] = "Servis kaydı ve semptomlar başarıyla oluşturuldu!";
        return RedirectToAction(model.ReturnAction, model.ReturnController, new { id = model.ReturnId });
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
                r.Vehicle.Client.Name.IndexOf(model.ClientName, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
        }
        if (!string.IsNullOrWhiteSpace(model.VehicleName))
        {
            records = records.Where(r =>
                r.Vehicle.Name.IndexOf(model.VehicleName, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
        }


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
            records = records.OrderByDescending(r => r.CreatedDate).ToList();
        }

        model.Records = records;

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        var mechanic = await _userManager.GetUserAsync(User);
        var record = await _serviceRecordService.GetOneAsync(id, mechanic.Id);
        if (record == null)
        {
            TempData["FailMessage"] = "Servis kaydı bulunamadı!";
            return RedirectToAction("Index");
        }
        else if (record.Status == "Tamamlandı")
        {
            TempData["FailMessage"] = "Tamamlanan kayıt iptal edilemez!";
            return RedirectToAction("Ongoing");
        }
        else if (record.Status == "İptal Edildi")
        {
            TempData["FailMessage"] = "Servis zaten iptal edilmiş!";
            return RedirectToAction("Ongoing");

        }

        record.Status = "İptal Edildi";
        var result = await _serviceRecordService.UpdateAsync();
        if (result > 0)
        {
            TempData["SuccessMessage"] = "Servis kaydı iptal edildi.";
            return RedirectToAction("Ongoing");
        }
        TempData["FailMessage"] = "Bir hata oluştu. Lütfen tekrar deneyin!";
        return RedirectToAction("Ongoing");

    }

   
    [HttpPost]
    public async Task<IActionResult> AddSymptomLog(RepairComment model)// comment ekleme metodu yazılacak
    {
        var mechanic = await _userManager.GetUserAsync(User);
        return View(model);
    }



}



