using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Services;
using PlayDiscGolf.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Controllers.User
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UserController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetUserInformation();
            return View(model);
        }            
    }
}
