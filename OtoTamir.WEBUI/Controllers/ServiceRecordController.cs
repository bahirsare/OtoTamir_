using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
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

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddByPlate(string plate)
    {
        var vehicle = await _vehicleService.GetOneAsync(plate);

        if (vehicle == null)
        {
            TempData["PlateNotFound"] = plate;
            return RedirectToAction("AddVehicle", "Vehicle"); // veya ViewComponent tetiklenebilir
        }

        //var model = new ServiceRecordViewModel
        //{
        //    VehicleId = vehicle.Id,
        //    VehiclePlate = vehicle.Plate,
        //    ClientName = vehicle.Client.Name
        //};

        return View("AddServiceRecord");
    }

    //[HttpPost]
    //public async Task<IActionResult> Save(ServiceRecordViewModel model)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View("AddServiceRecord", model);
    //    }

    //    await _serviceRecordService.CreateAsync(new ServiceRecord
    //    {
    //        VehicleId = model.VehicleId,
    //        Symptom = model.Symptom,
    //        Suggestion = model.Suggestion,
    //        EstimatedPrice = model.EstimatedPrice,
    //        CreatedDate = DateTime.Now
    //    });

    //    return RedirectToAction("Index", "ServiceRecord");
    //}
}
