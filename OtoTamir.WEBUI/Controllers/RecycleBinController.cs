using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;

namespace OtoTamir.WEBUI.Controllers
{
    public class RecycleBinController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IVehicleService _vehicleService;
        private readonly IServiceRecordService _serviceRecordService;
        private readonly IBankService _bankService;
        private readonly IBankCardService _bankCardService;
        private readonly IPosTerminalService _posTerminalService;

        public RecycleBinController(IClientService clientService, IVehicleService vehicleService, IServiceRecordService serviceRecordService,IBankService bankService, IBankCardService bankCardService, IPosTerminalService posTerminalService)
        {
            _clientService = clientService;
            _vehicleService = vehicleService;
            _serviceRecordService = serviceRecordService;
            _bankService = bankService;
            _bankCardService = bankCardService;
            _posTerminalService = posTerminalService;
        }

        public async Task<IActionResult> Index()
        {
            // Sadece silinenleri çekiyoruz
            var deletedClients = await _clientService.GetDeletedPagedAsync(null, q => q.OrderByDescending(x => x.CreatedDate), 1, 50);
            var deletedVehicles = await _vehicleService.GetDeletedPagedAsync(null, q => q.OrderByDescending(x => x.CreatedDate), 1, 50);
            var deletedServices = await _serviceRecordService.GetDeletedPagedAsync(null, q => q.OrderByDescending(x => x.CreatedDate), 1, 50);
            var deletedBanks = await _bankService.GetDeletedPagedAsync(null, q => q.OrderByDescending(x => x.CreatedDate), 1, 50);
            var deletedCards = await _bankCardService.GetDeletedPagedAsync(null, q => q.OrderByDescending(x => x.CreatedDate), 1, 50);
            var deletedPos = await _posTerminalService.GetDeletedPagedAsync(null, q => q.OrderByDescending(x => x.CreatedDate), 1, 50);

            // Verileri ViewBag (veya ViewModel) ile View'a gönderiyoruz
            ViewBag.DeletedClients = deletedClients.Results;
            ViewBag.DeletedVehicles = deletedVehicles.Results;
            ViewBag.DeletedServices = deletedServices.Results;
            ViewBag.deletedBanks = deletedBanks.Results;
            ViewBag.deletedCards = deletedCards.Results;
            ViewBag.deletedPos = deletedPos.Results;
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RestoreClient(int id)
        {
            await _clientService.RestoreAsync(id);
            TempData["SuccessMessage"] = "Müşteri başarıyla geri yüklendi!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RestoreVehicle(int id)
        {
            await _vehicleService.RestoreAsync(id);
            TempData["SuccessMessage"] = "Araç başarıyla geri yüklendi!";
            return RedirectToAction("Index");
        }

        // Diğer Restore metodları...
    }
}