using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;

namespace OtoTamir.WEBUI.Controllers
{
    public class ProcessesController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IVehicleService _vehicleService;

        public ProcessesController(IClientService clientService, IVehicleService vehicleService)
        {
            _clientService = clientService;
            _vehicleService = vehicleService;
        }
        public IActionResult AddNewProcess()
        {
            var clients = _clientService.GetAll();
            
            return View(clients);
        }
    }
}
