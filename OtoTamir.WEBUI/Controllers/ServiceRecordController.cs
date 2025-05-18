using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;

public class ServiceRecordController : Controller
{
    private readonly IVehicleService _vehicleService;
    private readonly IClientService _clientService;
    private readonly IServiceRecordService _serviceRecordService;

    public ServiceRecordController(IVehicleService vehicleService, IClientService clientService, IServiceRecordService serviceRecordService)
    {
        _vehicleService = vehicleService;
        _clientService = clientService;
        _serviceRecordService = serviceRecordService;
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
}