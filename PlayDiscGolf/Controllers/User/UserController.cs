using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Services;
using PlayDiscGolf.Services.User;
using PlayDiscGolf.ViewModels.User;
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
            var model = new UserInformationViewModel
            {
                UserID = await _accountService.GetInloggedUserIDAsync(),
                Email = await _accountService.GetEmailAsync(),
                Username = _accountService.GetUserName()
            };

            return View(model);
        }

        public async Task<IActionResult> SaveUserInformation(UserUpdateInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isEmailTaken = await _accountService.IsEmailTakenAsync(model.Email);
                var isUserNameTaken = await _accountService.IsUserNameTakenAsync(model.Username);

                if (!isEmailTaken && !isUserNameTaken)
                {
                    await _accountService.ChangeEmailAsync(model.Email);
                    await _accountService.ChangeUserNameAsync(model.Username);

                    return RedirectToAction("Index");
                }

                if (isEmailTaken) ModelState.AddModelError("Email", "Email is Taken");
                if (isUserNameTaken) ModelState.AddModelError("UserName", "UserName is Taken");
            }

            return View(model);
        }

        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.ChangePasswordAsync(model.Password);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> SearchUser(string query)
        {
            var user = await _accountService.GetUserByQueryAsync(query);

            if (ModelState.IsValid && user != null) return View(new UserClaimSearchResultModel { UserName = user.UserName });

            return View(new UserClaimSearchResultModel());
        }

        public async Task<IActionResult> ClaimScoreCard(UserInformationViewModel model)
        {
            await _userService.ClaimGamesFromUsernameAsync(model);

            return RedirectToAction("Index");
        }
    }
}
