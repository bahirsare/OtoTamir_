using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
using OtoTamir.CORE.DTOs.MechanicDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;
using OtoTamir.WEBUI.Services;
using OtoTamir.WEBUI.Services.MailHelper;

namespace OtotamirWEBUI.Controllers
{

    public class AccountController : Controller
    {
        private SignInManager<Mechanic> _signInManager;
        private UserManager<Mechanic> _userManager;
        private IMechanicService _mechanicService;
        private readonly IMapper _mapper;
        private readonly ITreasuryService _treasuryService;
        public AccountController(SignInManager<Mechanic> signInManager, UserManager<Mechanic> userManager, IMechanicService mechanicService, IMapper mapper, ITreasuryService treasuryService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mechanicService = mechanicService;
            _mapper = mapper;
            _treasuryService = treasuryService;
        }
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl == null ? "/Home/Index" : returnUrl });

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    if (user.Status == true)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

                        if (result.Succeeded)
                        {
                            if (!user.IsProfileCompleted)
                            {
                                return RedirectToAction("Profile", "Account");
                            }
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["ErrorMessage"] = ("Giriş Bilgilerinizi Kontrol Ediniz");
                    }

                    else
                    {
                        TempData["ErrorMessage"] = "Üyeliğiniz askıya alınmıştır. Lütfen yetkili ile iletişime geçiniz.";
                    }
                    return View(model);
                }
            }
            TempData["Message"] = "Lütfen bilgilerinizi eksiksiz doldurunuz!";
            return View(model);
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return View("Login");
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            EditProfileDTO model = new EditProfileDTO();

            _mapper.Map(user, model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(EditProfileDTO model, IFormFile? file)
        {
            ModelState.Remove("file");
            if (ModelState.IsValid)
            {
                
                var mechanic = await _userManager.GetUserAsync(User);
                var oldImage = mechanic.ImageUrl;
                if (file != null)
                {
                    mechanic.ImageUrl = await ImageOperations.UploadImageAsync(file);
                }
               
                _mapper.Map(model, mechanic);
                mechanic.IsProfileCompleted = true;
                var update = await _mechanicService.UpdateAsync();
                if (update == 1)
                {
                    TempData["SuccessMessage"] = "Profil güncelleme başarılı!";
                   if(oldImage!=null)
                        ImageOperations.DeleteImage(oldImage);
                }
                else
                {
                    TempData["FailMessage"] = "Profil güncelleme başarısız.";
                }
            }
            
           
            
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return RedirectToAction("Login", "Account");

                var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isOldPasswordCorrect)
                {
                    ModelState.AddModelError(string.Empty, "Mevcut şifre hatalı.");
                    return View("Profile", model);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View("Profile", model);
                }

                TempData["SuccessMessage"] = "Şifre güncelleme başarılı!";
                return RedirectToAction("Profile", "Account",result.Errors);
            }

            else
            {
                TempData["FailMessage"] = "Lütfen şifre alanlarını eksiksiz doldurun!";
                return RedirectToAction("Profile", "Account");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var password = _mechanicService.GenerateRandomPassword();

                var result = await _userManager.ResetPasswordAsync(user, token, password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return RedirectToAction("Login");
                }

               
                
                var body = $"Sayın <strong>{user.UserName};<br><br> Yeni şifreniz: <strong>{password} olarak belirlenmiştir.";

                MailHelper.SendMail(body, user.Email, "Şifre Yenileme");

                TempData["SuccessMessage"] = "Email adresinize gönderilen şifre yenileme linkine tıklayınız";
                return RedirectToAction("Login");
            }
            TempData["ErrorMessage"] = "Email ile kayıtlı kullanıcı bulunamadı";
            return RedirectToAction("Login");
        }
        [Authorize] 
        public async Task<IActionResult> FixMissingTreasury()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user.TreasuryId != null)
            {
                TempData["SuccessMessage"] = "Zaten bir kasanız var, işlem gerekmedi.";
                return RedirectToAction("Index", "Home");
            }

            
            var newTreasury = new Treasury
            {
               
                CashBalance = 0,
                
                ReceivablesBalance = 0,
                MechanicId = user.Id,
                
            };

            await _treasuryService.CreateAsync(newTreasury);

            // Kullanıcıyı güncelle
            user.TreasuryId = newTreasury.Id;
            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "Kasanız başarıyla oluşturuldu! Artık işlem yapabilirsiniz.";
            return RedirectToAction("Index", "Home");
        }
    }
}

