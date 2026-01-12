using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.WEBUI.Services;


namespace OtoTamir.WEBUI.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly IMechanicService _mechanicService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IMechanicService mechanicService, ILogger<AdminController> logger)
        {
            _mechanicService = mechanicService;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MechanicAsync()
        {
           var mechanics =await _mechanicService.GetAllAsync(includeClient:true,includeVehicle:true);
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
                _logger.LogInformation("Yeni tamirci/dükkan oluşturuldu: {StoreName}. Şifre oluşturuldu.", storeName);
                TempData["SuccessMessage"] = $"Tamirci başarıyla oluşturuldu. Şifresi: {result.Password}";
                return RedirectToAction("Mechanic", "Admin");
            }
            _logger.LogWarning("Tamirci oluşturma başarısız: {StoreName}. Hata: {Errors}", storeName, string.Join(", ", result.Errors));
            TempData["ErrorMessage"] = "Tamirci oluşturulurken bir hata oluştu. " + string.Join(", ", result.Errors);
            return RedirectToAction("Mechanic", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> EditStatusAsync(string id)
        {
            var mechanic = await _mechanicService.GetOneAsync(id);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Mechanic", "Admin");
            }

            mechanic.Status = (!mechanic.Status);
            var result = await _mechanicService.UpdateAsync();

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

        public async Task<IActionResult> Delete(string id)
        {
            var mechanic = await _mechanicService.GetOneAsync(id);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Mechanic", "Admin");
            }
            var result = await _mechanicService.DeleteAsync(id);
           
            if (result > 0)
            {
                _logger.LogWarning("Tamirci SİLİNDİ! Silinen ID: {Id}, Mağaza Adı: {StoreName} | Silen Admin: {AdminUser}",
            id, mechanic.StoreName, User.Identity?.Name ?? "Bilinmiyor");
                TempData["SuccessMessage"] = "Tamirci başarıyla silindi.";
                ImageOperations.DeleteImage(mechanic.ImageUrl);
            }
            else
            {
                _logger.LogError("Tamirci silinirken sistemsel hata oluştu. ID: {Id}", id);
                TempData["ErrorMessage"] = "Bir hata oluştu, tamirci silinemedi!";
            }
            return RedirectToAction("Mechanic", "Admin");
        }
    }
}
