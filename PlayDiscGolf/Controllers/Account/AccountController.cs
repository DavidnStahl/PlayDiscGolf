﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayDiscGolf.Models.ViewModels.Account;
using PlayDiscGolf.Services;

namespace PlayDiscGolf.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
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
        }
    }
}