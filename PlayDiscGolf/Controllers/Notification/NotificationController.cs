using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Core.Services.User;
using PlayDiscGolf.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Controllers.User
{
    public class NotificationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public NotificationController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var model = _mapper.Map<List<FriendViewModel>>(await _userService.GetFriendRequestsAsync());

            return View(model);
        }

        public IActionResult DeclineFriendRequest(string friendID)
        {
            _userService.DeclineFriendRequest(friendID);

            return RedirectToAction("Index");
        }

        public IActionResult AcceptFriendRequest(string friendID)
        {
            _userService.AcceptFriendRequest(friendID);

            return RedirectToAction("Index");
        }
        
    }
}

