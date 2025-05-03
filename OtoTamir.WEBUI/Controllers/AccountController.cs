using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.Profile;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;
using OtoTamir.WEBUI.Services;

namespace OtotamirWEBUI.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<Mechanic> _signInManager;
        private UserManager<Mechanic> _userManager;
        private IMechanicService _mechanicService;
        private readonly IMapper _mapper;
        public AccountController(SignInManager<Mechanic> signInManager, UserManager<Mechanic> userManager, IMechanicService mechanicService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mechanicService = mechanicService;
            _mapper = mapper;
        }
        public IActionResult Login()
        {
            return View();

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

                        TempData["Message"] = ("Giriş Bilgilerinizi Kontrol Ediniz");
                    }

                    else if (user.Status == false)
                    {
                        TempData["Message"] = "Üyeliğiniz askıya alınmıştır. Lütfen yetkili ile iletişime geçiniz.";
                    }
                    return View(model);
                }
            }
            TempData["Message"] = "Lütfen bilgilerinizi eksiksiz doldurunuz!";
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return View("Login");
        }

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
            string successMessage = "";
            string failMessage = "";
            ModelState.Remove("ImageUrl");
            ModelState.Remove("file");
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var mechanic = _mechanicService.GetOne(user.Id);

                if (file != null)
                {
                    model.ImageUrl = await ImageOperations.UploadImageAsync(file);
                }
                else
                {
                    model.ImageUrl = mechanic.ImageUrl;
                }
                _mapper.Map(model, mechanic);
                var update = _mechanicService.Update();
                if (update == 1)
                {
                    successMessage += "Profil güncelleme başarılı!";
                }
                else
                {
                    failMessage += "Profil güncelleme başarısız!";
                }


            }
            else
            {
                failMessage = "Formu tamamen doldurunuz.";
            }
            if (!string.IsNullOrEmpty(successMessage))
            {
                TempData["SuccessMessage"] = successMessage;
            }
            if (!string.IsNullOrEmpty(failMessage))
            {
                TempData["FailMessage"] = failMessage;
            }
            return View(model);
        }
        public async Task<IActionResult> ChangePassword()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(EditProfileDTO model)
        {
            string successMessage = "";
            var user = await _userManager.GetUserAsync(User);
            var mechanic = _mechanicService.GetOne(user.Id);
            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                else
                {
                    successMessage += "Şifre güncelleme başarılı! ";
                }
            }
            return RedirectToAction("Profile", "Account");
        }
    }
}
