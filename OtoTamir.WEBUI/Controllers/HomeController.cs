using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

using System.Diagnostics;
using System.Net.Http.Headers;

namespace OtotamirWEBUI.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IClientService _clientService;
        private readonly IMechanicService _mechanicService;
        private readonly UserManager<Mechanic> _userManager;

        public HomeController(IClientService clientService, IMechanicService mechanicService,UserManager<Mechanic>  userManager)
        {
            _clientService = clientService;
            _mechanicService = mechanicService;
            _userManager = userManager;
        }


       //[Authorize]
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Clients()

        {
            var clients = _clientService.GetAll();
            return View(clients);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateClientViewModel model)
        {
            

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Müþteri Eklenemedi, Lütfen Bilgileri Eksiksiz Doldurun";
                
                return RedirectToAction("Clients", "Home");
            }
            var mechanic = await _userManager.GetUserAsync(User);

            Client client = new Client()
            {
                Name = model.Name,
                Balance = model.Balance,
                PhoneNumber = model.PhoneNumber,
                Notes = model.Notes,
                MechanicId = mechanic.Id
            };

            var result = _clientService.Create(client);
            if (result == 1)
            {

                TempData["SuccessMessage"] = "Müþteri Eklendi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Müþteri Eklenemedi.";


            }

            return RedirectToAction("Clients", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
