using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayDiscGolf.Models.ViewModels.Account;
using PlayDiscGolf.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PlayDiscGolf.Controllers.Account
{
    public class AccountController : Controller
    {


            private readonly UserManager<IdentityUser> _userManager;
            private readonly SignInManager<IdentityUser> _signInManager;


            public AccountController(SignInManager<IdentityUser> signInManager,
                UserManager<IdentityUser> userManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;

            }
            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginViewModel model)
            {

                if (!ModelState.IsValid) { return View(model); }

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {

                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View(model);
                }

            }
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
    }

        /*public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.UserLoggingIn(model);

            if(result == true)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("Password", "Invalid login attempt");

            return View(model);
        }
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.UserRegister(model);

            if (result == true)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("Password", "Invalid Register attempt");

            return View(model);
        }*/
    
}
