using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Utilities;
using OtoTamir.WEBUI.Services;
using OtoTamir.WEBUI.Services.MailHelper;
using System.Linq.Expressions;


namespace OtoTamir.WEBUI.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private SignInManager<Mechanic> _signInManager;
        private readonly IMechanicService _mechanicService;
        private readonly IMailHelper _mailHelper;
        private readonly IAnnouncementService _announcementService;
        private UserManager<Mechanic> _userManager;
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(SignInManager<Mechanic> signInManager,IMechanicService mechanicService, ILogger<AdminController> logger,IMailHelper mailHelper,UserManager<Mechanic> userManager, IAnnouncementService announcementService,RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _mechanicService = mechanicService;
            _logger = logger;
            _userManager = userManager;
            _mailHelper = mailHelper;
            _announcementService = announcementService;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            
            var allMechanics = await _userManager.Users
                .Include(m => m.Clients)
                .Where(m => !m.IsDeleted)
                .ToListAsync();

            
            ViewBag.TotalMechanics = allMechanics.Count;

           
            ViewBag.ActiveMechanics = allMechanics.Count(m => m.Status);

            
            ViewBag.TotalClients = allMechanics.Sum(m => m.Clients?.Count ?? 0);

            
            ViewBag.ExpiringCount = allMechanics.Count(m => m.SubscriptionEndDate <= DateTime.Now.AddDays(7));

            
            ViewBag.LatestMechanics = allMechanics
                .OrderByDescending(m => m.CreatedDate)
                .Take(5)
                .ToList();

            ViewBag.ExpiringMechanics = allMechanics
                .Where(m => m.SubscriptionEndDate <= DateTime.Now.AddDays(7))
                .OrderBy(m => m.SubscriptionEndDate)
                .Take(5)
                .ToList();

            return View();
        }
        public async Task<IActionResult> Mechanic(string search = null, int page = 1)
        {
            ViewBag.Search = search;

            var pagedMechanics = await _mechanicService.GetPagedAsync(
                filter: string.IsNullOrEmpty(search) ? null : x => x.UserName.Contains(search) || x.StoreName.Contains(search) && !x.IsDeleted,
                orderBy: q => q.OrderByDescending(x => x.CreatedDate),
                page: page,
                pageSize: 10,
                includes: x => x.Clients 
            );
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            ViewBag.AdminIds = adminUsers.Select(x => x.Id).ToList();

            return View(pagedMechanics);
            
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
                TempData["SuccessMessage"] = $"Tamirci başarıyla oluşturuldu. Şifresi:  <b>{result.Password} </b>";
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
        public async Task<IActionResult> RecycleBin(int page = 1)
        {
            var pagedDeletedMechanics = await _mechanicService.GetDeletedPagedAsync(null,null,page, 10);
            return View(pagedDeletedMechanics);
        }

        [HttpPost]
        public async Task<IActionResult> RestoreMechanic(string id)
        {
            var result = await _mechanicService.RestoreMechanicAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Usta hesabı başarıyla geri yüklendi ve aktifleştirildi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Usta bulunamadı!";
            }
            return RedirectToAction("RecycleBin");
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
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] string mechanicId)
        {
            var mechanic = await _userManager.FindByIdAsync(mechanicId);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Usta bulunamadı!";
                return RedirectToAction("Mechanic");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(mechanic);
            var newPassword = _mechanicService.GenerateRandomPassword();
            var result = await _userManager.ResetPasswordAsync(mechanic, token, newPassword);

            if (result.Succeeded)
            {
                string mailInfoText = "";

                // 1. Mail varsa gönder ve bilgi metnini ayarla
                if (!string.IsNullOrEmpty(mechanic.Email))
                {
                    var body = $"Sayın <strong>{mechanic.UserName}</strong>;<br><br> Yönetici tarafından şifreniz sıfırlanmıştır.<br>Yeni şifreniz: <strong>{newPassword}</strong>";
                    _mailHelper.SendMail(body, mechanic.Email, "OtoTamir - Şifre Sıfırlama");

                    mailInfoText = $"<div class='text-success small mt-2'><i class='bi bi-envelope-check-fill'></i> Ayrıca <b>{mechanic.Email}</b> adresine de e-mail olarak iletildi.</div>";
                }
                // 2. Mail yoksa uyarı metni ayarla
                else
                {
                    mailInfoText = "<div class='text-warning small mt-2'><i class='bi bi-exclamation-triangle-fill'></i> Kullanıcının e-mail adresi bulunmuyor, şifreyi iletmeyi unutmayın.</div>";
                }

                // 3. HER DURUMDA ŞİFREYİ EKRANDA GÖSTER
                TempData["SuccessMessage"] = $@"
            <div class='text-center mt-3'>
                <p><strong>{mechanic.StoreName}</strong> adlı kullanıcının şifresi sıfırlandı.</p>
                <h2 class='fw-bold text-danger my-3 py-3 bg-light border border-danger rounded' style='letter-spacing: 3px;'>{newPassword}</h2>
                {mailInfoText}
            </div>";
            }
            else
            {
                var errors = string.Join("<br/>", result.Errors.Select(e => e.Description));
                TempData["ErrorMessage"] = $"Şifre sıfırlanırken hata oluştu:<br/>{errors}";
            }

            return RedirectToAction("Mechanic");
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsMechanic([FromForm] string mechanicId)
        {
            var mechanic = await _userManager.FindByIdAsync(mechanicId);
            if (mechanic != null)
            {
               
                await _signInManager.SignOutAsync();

               
                await _signInManager.SignInAsync(mechanic, isPersistent: false);

                
                return RedirectToAction("Mechanic", "Home");
            }

            TempData["ErrorMessage"] = "Giriş yapılacak usta bulunamadı!";
            return RedirectToAction("Mechanic");
        }
        
        public async Task<IActionResult> Announcements(int page = 1)
        {
            var pagedAnnouncements = await _announcementService.GetPagedAsync(
                orderBy: q => q.OrderByDescending(x => x.CreatedDate),
                page: page,
                pageSize: 10
            );

            return View(pagedAnnouncements);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement(string title, string message, string type)
        {
           
            var activeAnnouncements = await _announcementService.GetPagedAsync(
                filter: x => x.IsActive,
                page: 1,
                pageSize: 50
            );

            
            if (activeAnnouncements.Results != null && activeAnnouncements.Results.Any())
            {
                foreach (var item in activeAnnouncements.Results)
                {
                    item.IsActive = false;
                }
                await _announcementService.UpdateAsync(); 
            }

            
            var newAnnouncement = new Announcement
            {
                Title = title,
                Message = message,
                Type = type,
                IsActive = true
            };

            
            await _announcementService.CreateAsync(newAnnouncement);

            TempData["SuccessMessage"] = "Duyuru başarıyla yayınlandı ve tüm ustaların ekranına düştü!";
            return RedirectToAction("Announcements");
        }

        
        [HttpPost]
        public async Task<IActionResult> ToggleAnnouncement(int id)
        {
            
            var result = await _announcementService.GetPagedAsync(filter: x => x.Id == id, page: 1, pageSize: 1);
            var announcement = result.Results?.FirstOrDefault();

            if (announcement != null)
            {
                announcement.IsActive = !announcement.IsActive;
                await _announcementService.UpdateAsync(); 
            }

            return RedirectToAction("Announcements");
        }

        [HttpPost]
        public async Task<IActionResult> ExtendSubscription([FromForm] string mechanicId, [FromForm] int months)
        {
            var mechanic = await _userManager.FindByIdAsync(mechanicId);
            if (mechanic == null)
            {
                TempData["ErrorMessage"] = "Usta bulunamadı!";
                return RedirectToAction("Mechanic");
            }

            if (mechanic.SubscriptionEndDate < DateTime.Now)
            {
                mechanic.SubscriptionEndDate = DateTime.Now;
            }

            mechanic.SubscriptionEndDate = mechanic.SubscriptionEndDate.AddMonths(months);
            var result = await _userManager.UpdateAsync(mechanic);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"<strong>{mechanic.StoreName}</strong> adlı ustanın lisans süresi <strong>{months} ay</strong> uzatıldı.<br>Yeni bitiş tarihi: <strong>{mechanic.SubscriptionEndDate:dd.MM.yyyy}</strong>";
            }
            else
            {
                TempData["ErrorMessage"] = "Süre uzatılırken bir hata oluştu.";
            }

            return RedirectToAction("Mechanic");
        }

        public async Task<IActionResult> ToggleAdminRole([FromForm] string mechanicId, [FromForm] bool isGranted)
        {
            var mechanic = await _userManager.FindByIdAsync(mechanicId);
            if (mechanic == null) return RedirectToAction("Index");

            // 1. GÜVENLİK: Eğer veritabanında "Admin" diye bir rol hiç oluşturulmadıysa, önce onu yarat.
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. Yetki veriliyorsa
            if (isGranted)
            {
                if (!await _userManager.IsInRoleAsync(mechanic, "Admin"))
                {
                    await _userManager.AddToRoleAsync(mechanic, "Admin");
                }
                TempData["SuccessMessage"] = $"<i class='bi bi-star-fill text-warning'></i> <b>{mechanic.StoreName}</b> hesabına <b>Admin</b> yetkisi verildi!";
            }
            // 3. Yetki alınıyorsa
            else
            {
                if (await _userManager.IsInRoleAsync(mechanic, "Admin"))
                {
                    await _userManager.RemoveFromRoleAsync(mechanic, "Admin");
                }
                TempData["SuccessMessage"] = $"<b>{mechanic.StoreName}</b> hesabının Admin yetkisi geri alındı.";
            }

            return RedirectToAction("Mechanic");
        }

    }
}
