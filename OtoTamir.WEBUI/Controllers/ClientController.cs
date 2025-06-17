using Microsoft.AspNetCore.Mvc;

namespace OtoTamir.WEBUI.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
