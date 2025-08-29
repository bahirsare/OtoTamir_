using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
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
    private readonly IBalanceLogService _balanceLogService;

    public ServiceRecordController(IVehicleService vehicleService, IClientService clientService, IServiceRecordService serviceRecordService, ISymptomService symptomService, IMapper mapper, UserManager<Mechanic> userManager,IBalanceLogService balanceLogService)
    {
        _vehicleService = vehicleService;
        _clientService = clientService;
        _serviceRecordService = serviceRecordService;
        _symptomService = symptomService;
        _mapper = mapper;
        _userManager = userManager;
        _balanceLogService= balanceLogService;
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
    public async Task<IActionResult> GetClientDetails(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var clients = await _clientService.GetAllAsync(user.Id, true, false, i => i.Id != id);
        var model = new ListClientDTO
        {
            Clients = clients,
            SelectedClientId = id,

        };
        model.SelectedClientName = (await _clientService.GetOneAsync((int)model.SelectedClientId, user.Id, false, false)).Name;
        if (clients.Count == 0)
        {
            clients = await _clientService.GetAllAsync(user.Id, false, false);
        }
        return View("Index", model);

    }
    [HttpPost]
    public async Task<IActionResult> AddServiceWorkflowLogs(ServiceWorkflowLogDTO WorkflowLogDTO, BalanceManager balanceManager)
    {
        List<string> URL = WorkflowLogDTO.ReturnUrl.Split('/').ToList();
        if (!ModelState.IsValid)
        {
            TempData["FailMessage"] = "Lütfen kaydı eksiksiz doldurun.";
            return RedirectToAction(URL[2], URL[1]);

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
            return RedirectToAction(URL[2], URL[1]);
        }
        if( symptom.Status!="Devam Ediyor")
        {
            TempData["FailMessage"] = "Tamamlanmış ya da İptal edilmiş işlemlere güncelleme yapılamaz.";
            return RedirectToAction(URL[2], URL[1]);
        }
        if (WorkflowLogDTO.AdditionalDays != null || WorkflowLogDTO.AdditionalCost != null)
        {
            if (WorkflowLogDTO.AdditionalDays != null&&WorkflowLogDTO.AdditionalDays>0)
            {
                symptom.EstimatedDaysToFix += (int)WorkflowLogDTO.AdditionalDays;
            }

            if (WorkflowLogDTO.AdditionalCost != null && WorkflowLogDTO.AdditionalCost > 0)
            {
                symptom.EstimatedCost += (decimal)WorkflowLogDTO.AdditionalCost;
            }

            string log = "";

            if (WorkflowLogDTO.AdditionalDays != null)
            {
                log += $" +{WorkflowLogDTO.AdditionalDays} gün";
            }

            if (WorkflowLogDTO.AdditionalCost != null)
            {
                log += $" +{WorkflowLogDTO.AdditionalCost:C} ek maliyet";
            }

            WorkflowLogDTO.Content += $" ({log.Trim()} eklendi)";
        }


        var WorkflowLog = _mapper.Map<RepairComment>(WorkflowLogDTO);
        WorkflowLog.ModifiedDate = DateTime.Now;
        WorkflowLog.CreatedDate = DateTime.Now;
        symptom.Status = WorkflowLog.Status;
        symptom.ServiceWorkflowLogs.Add(WorkflowLog);

        

        
        await _serviceRecordService.UpdateStatusAsync(symptom.ServiceRecordId,user.Id);
        
        TempData["SuccessMessage"] = "İşlem başarılı! Kayıt güncellendi.";
        var record = await _serviceRecordService.GetOneAsync(symptom.ServiceRecordId, user.Id,true, false);
        if (record.Status == "Tamamlandı")
        {
             var client = await _clientService.GetOneAsync(record.Vehicle.ClientId, user.Id, false, false);
            var result = await balanceManager.UpdateBalanceAsync(client,record.Price);

            var balanceLogResult = await _balanceLogService.CreateAsync(result.BalanceLogs[0]);


            if (balanceLogResult > 0)
            {
                TempData["SuccessMessage"] = "Bakiye hareketi başarıyla kaydedildi.";
            }
            else
            {
                TempData["FailMessage"] = "Kayıt sırasında hata oluştu.";
            }
            await _clientService.UpdateAsync();
        }


        return RedirectToAction(URL[2], URL[1]);
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
        if (model.IsCompleted) {
            await _serviceRecordService.UpdateStatusAsync(serviceRecord.Id, user.Id);
            
        }

        foreach (var item in model.Symptoms)
        {
            var symptom = _mapper.Map<Symptom>(item);
            symptom.ServiceRecordId = serviceRecord.Id;
            symptom.Status = "Devam Ediyor";


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

        if (string.IsNullOrEmpty(model.CurrentStatus))
            model.CurrentStatus = "Devam Ediyor";

        if (!string.IsNullOrWhiteSpace(model.CurrentStatus) && model.CurrentStatus != "Tümü")
            filter = filter.AndAlso(sr => sr.Status == model.CurrentStatus);

        if (model.StartDate.HasValue)
            filter = filter.AndAlso(sr => sr.CreatedDate >= model.StartDate.Value);

        if (model.EndDate.HasValue)
            filter = filter.AndAlso(sr => sr.CreatedDate <= model.EndDate.Value);

        var records = await _serviceRecordService.GetAllAsync(
            mechanic.Id,
            includeVehicle: true,
            includeClient: true,
            includeSymptoms: false,
            filter: filter
        );

        if (!string.IsNullOrWhiteSpace(model.ClientName))
            records = records.Where(r => r.Vehicle.Client.Name.IndexOf(model.ClientName, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();

        if (!string.IsNullOrWhiteSpace(model.VehicleName))
            records = records.Where(r => r.Vehicle.Name.IndexOf(model.VehicleName, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();

        
        string sortCol = model.SortColumn?.Trim() ?? "CreatedDate";
        string sortDir = model.SortDirection?.ToLower() ?? "desc";

        records = (sortCol, sortDir) switch
        {
            ("ClientName", "asc") => records.OrderBy(r => r.Vehicle.Client.Name).ToList(),
            ("ClientName", "desc") => records.OrderByDescending(r => r.Vehicle.Client.Name).ToList(),

            ("VehicleName", "asc") => records.OrderBy(r => r.Vehicle.Name).ToList(),
            ("VehicleName", "desc") => records.OrderByDescending(r => r.Vehicle.Name).ToList(),

            ("Plate", "asc") => records.OrderBy(r => r.Vehicle.Plate).ToList(),
            ("Plate", "desc") => records.OrderByDescending(r => r.Vehicle.Plate).ToList(),

            ("Status", "asc") => records.OrderBy(r => r.Status).ToList(),
            ("Status", "desc") => records.OrderByDescending(r => r.Status).ToList(),

            ("CreatedDate", "asc") => records.OrderBy(r => r.CreatedDate).ToList(),
            ("CreatedDate", "desc") => records.OrderByDescending(r => r.CreatedDate).ToList(),

            ("ModifiedDate", "asc") => records.OrderBy(r => r.ModifiedDate).ToList(),
            ("ModifiedDate", "desc") => records.OrderByDescending(r => r.ModifiedDate).ToList(),

            ("CompletedDate", "asc") => records.OrderBy(r => r.CompletedDate).ToList(),
            ("CompletedDate", "desc") => records.OrderByDescending(r => r.CompletedDate).ToList(),

            _ => records.OrderByDescending(r => r.CreatedDate).ToList()
        };

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
        record.CompletedDate = DateTime.Now;
        record.ModifiedDate = DateTime.Now;

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



