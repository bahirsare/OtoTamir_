using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
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

        public HomeController(IClientService clientService, UserManager<Mechanic> userManager, IMapper mapper)
        {
            _clientService = clientService;

            _userManager = userManager;

            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }

        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
