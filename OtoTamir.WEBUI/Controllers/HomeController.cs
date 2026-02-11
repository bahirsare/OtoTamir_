using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

using System.Diagnostics;

namespace OtotamirWEBUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IClientService _clientService;

        private readonly UserManager<Mechanic> _userManager;

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IServiceRecordService _serviceRecordService;
        private readonly IVehicleService _vehicleService;

        public HomeController(IClientService clientService, UserManager<Mechanic> userManager, IMapper mapper, ILogger logger, IServiceRecordService serviceRecordService, IVehicleService vehicleService)
        {
            _clientService = clientService;

            _userManager = userManager;

            _mapper = mapper;
            _logger = logger;
            _serviceRecordService = serviceRecordService;
            _vehicleService = vehicleService;
        }


        public async Task<IActionResult> Index()
        {
            var mechanic = await _userManager.GetUserAsync(User);
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var model = new DashboardViewModel();

            // 1. KART VERÝLERÝ (Ýstatistikler)

            // Tamamlanmamýþ (Aktif) Araçlar
            model.ActiveVehicleCount = await _serviceRecordService.GetAllAsync(mechanic.Id,true,false,true).CountAsync(x => x.Status == ServiceStatus.InProgress || x.Status == ServiceStatus.Pending);

            // Bugün Tamamlanan ve Parasý Alýnanlar (Basit mantýk)
            // NOT: Eðer Transaction tablon varsa oradan çekmek daha doðrudur. 
            // Þimdilik ServiceRecord üzerinden gidiyoruz:
            model.TodayIncome = await _context.ServiceRecords
                .Where(x => x.Status == ServiceStatus.Completed && x.CompletedDate >= today)
                .SumAsync(x => x.Price);

            // Bu Ayýn Cirosu
            model.MonthlyIncome = await _context.ServiceRecords
                .Where(x => x.Status == ServiceStatus.Completed && x.CompletedDate >= startOfMonth)
                .SumAsync(x => x.Price);

            // Toplam Alacak (Müþterilerin Bakiyesi)
            // Negatif bakiye = Müþterinin alacaðý, Pozitif = Müþterinin borcu
            // Bize borcu olanlarýn toplamýný alalým.
            model.TotalDebt = await _context.Clients
                .Where(x => x.Balance > 0)
                .SumAsync(x => x.Balance);

            // 2. SON GELEN 5 ARAÇ LÝSTESÝ
            model.RecentServices = await _context.ServiceRecords
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.Client)
                .OrderByDescending(x => x.CreatedDate) // En yeni en üstte
                .Take(5)
                .ToListAsync();

            return View(model);
        }

        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
