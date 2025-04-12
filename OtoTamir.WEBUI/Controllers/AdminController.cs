using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;

namespace OtoTamir.WEBUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMechanicService _mechanicService;
        public AdminController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }

        public IActionResult Index()
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
                return RedirectToAction(nameof(Index));
            }

            var result = await _mechanicService.CreateMechanicAsync(storeName);
            if (result.Success)
            {
                TempData["SuccessMessage"] = $"Tamirci başarıyla oluşturuldu. Şifresi: {result.Password}";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Tamirci oluşturulurken bir hata oluştu. " + string.Join(", ", result.Errors);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult EditStatus(string id, bool status)
        {
            var mechanic = _mechanicService.GetOne(id);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            mechanic.Status = status;
            var result = _mechanicService.Update(); // Burada update metodunda durumu güncelliyoruz

            if (result > 0)
            {                
                TempData["SuccessMessage"] = "Durum başarıyla güncellendi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Bir hata oluştu, durumu güncelleyemedik.";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            var mechanic = _mechanicService.GetOne(id);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }
    }
}
