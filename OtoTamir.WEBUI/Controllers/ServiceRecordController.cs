using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;

public class ServiceRecordController : Controller
{
    private readonly IVehicleService _vehicleService;
    private readonly IClientService _clientService;
    private readonly IServiceRecordService _serviceRecordService;
    private readonly ISymptomService _SymptomService;

    public ServiceRecordController(IVehicleService vehicleService, IClientService clientService, IServiceRecordService serviceRecordService, ISymptomService symptomService)
    {
        _vehicleService = vehicleService;
        _clientService = clientService;
        _serviceRecordService = serviceRecordService;
        _SymptomService=symptomService;
    }


    public async Task<IActionResult> Index()
    {
        var clients = await _clientService.GetAllAsync();
        var model = new ListClientDTO
        {
            Clients = clients,
       
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> GetVehiclesByClientId(int selectedClientId)
    {
        var clients = await _clientService.GetAllAsync();
        var model = new ListClientDTO
        {
            Clients = clients,
            SelectedClientId = selectedClientId
        };
        if (clients.Count == 0)
        {
            clients = await _clientService.GetAllAsync();
        }
        return View("Index", model);
    }
    [HttpPost]
    public async Task<IActionResult> AddServiceRecord(ServiceRecord model)
    {

        ServiceRecord serviceRecord = new ServiceRecord()
        { 
            Status="Created",


        };

        return View();
    }
}