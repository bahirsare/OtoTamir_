using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

namespace OtotamirWEBUI.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<Mechanic> _signInManager;
        private UserManager<Mechanic> _userManager;
        public AccountController(SignInManager<Mechanic> signInManager, UserManager<Mechanic> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                        if (user.IsProfileCompleted)
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

        public IActionResult Profile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Profile(ProfileViewModel model)
        {
            return View();
        }
    }
}
