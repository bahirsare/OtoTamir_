using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;

using OtoTamir.WEBUI.Models;

using System.Diagnostics;
using System.Net.Http.Headers;

namespace OtotamirWEBUI.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IClientService _clientService;
        private readonly IMechanicService _mechanicService;

        public HomeController(IClientService clientService, IMechanicService mechanicService)
        {
            _clientService = clientService;
            _mechanicService = mechanicService;
        }

       

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Clients()

        {
            var clients = _clientService.GetAll();
            return View(clients);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
