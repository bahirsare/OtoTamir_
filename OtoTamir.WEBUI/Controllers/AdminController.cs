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

        public AdminController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
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

                TempData["SuccessMessage"] = $"Tamirci başarıyla oluşturuldu. Şifresi: {result.Password}";
                return RedirectToAction("Mechanic", "Admin");
            }

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
                TempData["SuccessMessage"] = "Tamirci başarıyla silindi.";
                ImageOperations.DeleteImage(mechanic.ImageUrl);
            }
            else
            {
                TempData["ErrorMessage"] = "Bir hata oluştu, tamirci silinemedi!";
            }
            return RedirectToAction("Mechanic", "Admin");
        }
    }
}
