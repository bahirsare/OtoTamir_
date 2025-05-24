using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.DTOs.SymptomDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;
[Authorize]
public class ServiceRecordController : Controller
{
    private readonly IVehicleService _vehicleService;
    private readonly IClientService _clientService;
    private readonly IServiceRecordService _serviceRecordService;
    private readonly ISymptomService _symptomService;
    private readonly IMapper _mapper;
    private readonly UserManager<Mechanic> _userManager;

    public ServiceRecordController(IVehicleService vehicleService, IClientService clientService, IServiceRecordService serviceRecordService, ISymptomService symptomService,IMapper mapper, UserManager<Mechanic> userManager)
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
        var clients = await _clientService.GetAllAsync(user.Id,false,false);
        var model = new ListClientDTO
        {
            Clients = clients,
            SelectedClientId = selectedClientId
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> GetVehiclesByClientId(int selectedClientId)
    {
        var user = await _userManager.GetUserAsync(User);
        var clients = await _clientService.GetAllAsync(user.Id,true,false);
        var model = new ListClientDTO
        {
            Clients = clients,
            SelectedClientId = selectedClientId
        };
        if (clients.Count == 0)
        {
            clients = await _clientService.GetAllAsync(user.Id,false,false);
        }
        return View("Index", model);
    }
    [HttpPost]
    public async Task<IActionResult> CreateServiceRecord(CreateSymptomGroupDTO model)
    {
        var user = await _userManager.GetUserAsync(User);
        var vehicle = await _vehicleService.GetOneAsync(id:model.VehicleId,mechanicId:user.Id); 
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
            AuthorName=model.AuthorName
            
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
            
           var symptomResult  =await _symptomService.CreateAsync(symptom);
           // serviceRecord.SymptomList.Add(symptom);
        }



        return RedirectToAction("Index", new { selectedClientId = clientId });
    }

    public async Task<IActionResult> Ongoing(string ClientName, string CurrentStatus)
    {
        var mechanicId = _userManager.GetUserId(User);

        var query = _serviceRecordService.GetAllAsync()
            .Include(sr => sr.Vehicle)
                .ThenInclude(v => v.Client)
            .Where(sr => sr.Vehicle.Client.MechanicId == mechanicId);

        if (!string.IsNullOrWhiteSpace(ClientName))
        {
            query = query.Where(sr => sr.Vehicle.Client.Name.Contains(ClientName));
        }

        if (!string.IsNullOrWhiteSpace(CurrentStatus))
        {
            query = query.Where(sr => sr.Status == CurrentStatus);
        }

        var records = await query
            .OrderByDescending(sr => sr.CreatedDate)
            .ToListAsync();

        var model = new ServiceRecordOngoingViewModel
        {
            Records = records,
            ClientName = ClientName,
            CurrentStatus = CurrentStatus
        };

        return View(model);
    }




}



