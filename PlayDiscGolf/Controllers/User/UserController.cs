using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Core.Services.User;
using PlayDiscGolf.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Controllers.User
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IAccountService accountService, IMapper mapper)
        {
            _userService = userService;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var model = new UserInformationViewModel
            {
                UserID = await _accountService.GetInloggedUserIDAsync(),
                Email = await _accountService.GetEmailAsync(),
                Username = _accountService.GetUserName(),
                Friends = _mapper.Map<List<FriendViewModel>>(await _userService.GetFriendsAsync()).Where(x => x.FriendRequestAccepted == true).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchUser([Bind("SearchUsername")] UserInformationViewModel formModel)
        {
            var userName = await _userService.SearchUsersAsync(formModel.SearchUsername);

            var model = new UserSearchResultViewModel
            {
                UserName = userName
            };

            return PartialView("_SearchUserNameResult", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(UserChangeEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isEmailTaken = await _accountService.IsEmailTakenAsync(model.Email);

                if (!isEmailTaken)
                {
                    await _accountService.ChangeEmailAsync(model.Email);

                    return Json(new { success = true, responseText = "Your message successfuly sent!"});
                }

                if (isEmailTaken) ModelState.AddModelError("Email", "Email is Taken");
            }

            return PartialView("_ChangeEmail", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.ChangePasswordAsync(model.Password);

                return Json(new { success = true, responseText = "Your message successfuly sent!"});
            }

            return PartialView("_ChangePassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(UserChangeUsernameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUserNameTaken = await _accountService.IsUserNameTakenAsync(model.Username);

                if (!isUserNameTaken)
                {
                    await _accountService.ChangeUserNameAsync(model.Username);

                    return Json(new { success = true, responseText = "Your message successfuly sent!" });
                }

                if (isUserNameTaken) ModelState.AddModelError("Username", "Username is Taken");
            }

            return PartialView("_ChangeUsername", model);
        }

        public async Task<IActionResult> AddFriend(string username)
        {
            await _userService.SendFriendRequestAsync(username);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFriend(string friendID)
        {
            await _userService.RemoveFriendAsync(friendID);

            return RedirectToAction("Index");
        }
    }
}
