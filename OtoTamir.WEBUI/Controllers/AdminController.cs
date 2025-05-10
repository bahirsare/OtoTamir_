using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;

namespace OtoTamir.WEBUI.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly IMechanicService _mechanicService;

        public AdminController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Mechanic()
        {
            var mechanics = _mechanicService.GetAll();
            return View(mechanics);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string storeName)
        {
            if (string.IsNullOrEmpty(storeName))
            {
                TempData["ErrorMessage"] = "Geçerli bir kullanıcı adı giriniz.";
                return RedirectToAction("Mechanic", "Admin");
            }

            var result = await _mechanicService.CreateMechanicAsync(storeName);
            if (result.Success)
            {
                TempData["SuccessMessage"] = $"Tamirci başarıyla oluşturuldu. Şifresi: {result.Password}";
                return RedirectToAction("Mechanic", "Admin");
            }

            TempData["ErrorMessage"] = "Tamirci oluşturulurken bir hata oluştu. " + string.Join(", ", result.Errors);
            return RedirectToAction("Mechanic", "Admin");
        }
        [HttpPost]
        public IActionResult EditStatus(string id)
        {
            var mechanic = _mechanicService.GetOne(id);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Mechanic", "Admin");
            }

            mechanic.Status = (!mechanic.Status);
            var result = _mechanicService.Update();

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Durum başarıyla güncellendi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Bir hata oluştu, durumu güncelleyemedik.";
            }

            return RedirectToAction("Mechanic", "Admin");
        }

        public IActionResult Delete(string id)
        {
            var mechanic = _mechanicService.GetOne(id);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Mechanic", "Admin");
            }
            var result = _mechanicService.Delete(id);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Tamirci başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Bir hata oluştu, tamirci silinemedi!";
            }
            return RedirectToAction("Mechanic", "Admin");
        }
    }
}
