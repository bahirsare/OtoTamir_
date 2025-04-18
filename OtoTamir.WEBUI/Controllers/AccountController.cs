using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

namespace OtotamirWEBUI.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<Mechanic> _signInManager;
        private UserManager<Mechanic> _userManager;
        private IMechanicService _mechanicService;
        public AccountController(SignInManager<Mechanic> signInManager, UserManager<Mechanic> userManager, IMechanicService mechanicService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mechanicService = mechanicService;
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
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

                    if (result.Succeeded)
                    {
                        if (!user.IsProfileCompleted)
                        {
                            return RedirectToAction("Profile", "Account");
                        }
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "Giriş Bilgilerinizi Kontrol Ediniz");


                    return View(model);
                }
            }
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
            ProfileViewModel model = new ProfileViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                StoreName = user.StoreName,
                PhoneNumber = user.PhoneNumber,
                Adress = user.Adress,
                Skills = user.Skills,
                Image = user.Image

            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)

        {

            string successMessage = "";
            string failMessage = "";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);



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
            user.Email = model.Email;
            user.StoreName = model.StoreName;
            user.PhoneNumber = model.PhoneNumber;
            user.Adress = model.Adress;
            user.Skills = model.Skills;
            user.IsProfileCompleted = true;
            user.Image = model.Image;

            var update = _mechanicService.Update();
            if (update == 1)
            {
                successMessage += "Profil güncelleme başarılı!";
            }
            else
            {
                failMessage += "Profil güncelleme başarısız!";
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
    }
}
