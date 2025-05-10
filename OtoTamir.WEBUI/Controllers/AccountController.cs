using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.MechanicDTOs;

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

                    else
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

                TempData["Message"] = "Şifre güncelleme başarılı!";
                return RedirectToAction("Profile", "Account");
            }

            else
            {
                return RedirectToAction("Profile", "Account");
            }
        }
    }
}

