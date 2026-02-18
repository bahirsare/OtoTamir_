using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITreasuryService _treasuryService;

        private readonly UserManager<Mechanic> _userManager;


        private readonly ILogger<HomeController> _logger;
        private readonly IServiceRecordService _serviceRecordService;


        public HomeController(IClientService clientService, UserManager<Mechanic> userManager, ILogger<HomeController> logger, IServiceRecordService serviceRecordService, ITreasuryService treasuryService)
        {
            _clientService = clientService;
            _treasuryService = treasuryService;
            _userManager = userManager;
            _logger = logger;
            _serviceRecordService = serviceRecordService;

        }


        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Dashboard (Index) page requested.");

            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {

                    _logger.LogWarning("Dashboard accessed but User is null. Redirecting to Login.");
                    return RedirectToAction("Login", "Account");
                }


                _logger.LogInformation("Loading dashboard data for Mechanic ID: {MechanicId}, Store: {StoreName}", user.Id, user.StoreName);

                var model = new DashboardViewModel();

                var todayStart = DateTime.Today;
                
                model.ActiveVehicleCount = await _serviceRecordService.CountByStatusAsync(user.Id, ServiceStatus.InProgress);
               
                model.CompletedJobCount = await _serviceRecordService.CountByStatusAsync(user.Id, ServiceStatus.Completed, todayStart);
                model.CancelledJobCount = await _serviceRecordService.CountByStatusAsync(user.Id, ServiceStatus.Cancelled, todayStart);
                model.TodayJobCount = await _serviceRecordService.CountByStatusAsync(user.Id, null, todayStart);

                model.TodayIncome = await _serviceRecordService.GetTotalIncomeAsync(user.Id, "today");
                model.MonthlyIncome = await _serviceRecordService.GetTotalIncomeAsync(user.Id, "month");
                model.YearlyIncome= await _serviceRecordService.GetTotalIncomeAsync(user.Id, "year");
                var treasuryData = await _treasuryService.GetOneAsync(user.TreasuryId,user.Id);
                model.CashBalance = treasuryData.BankBalance;

                model.BankBalance = treasuryData.CashBalance;
                model.TotalDebt = treasuryData.ReceivablesBalance;

                model.RecentServices = await _serviceRecordService.GetLastRecordsAsync(user.Id, 5);


               

                _logger.LogInformation("Dashboard data loaded successfully. Today's Income: {TodayIncome}", model.TodayIncome);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while loading the Dashboard.");


                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError("Error page requested. Request ID: {RequestId}", requestId);

            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}
