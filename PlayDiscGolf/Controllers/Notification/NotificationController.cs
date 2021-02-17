using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

            if(model.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult DeclineFriendRequest(string friendID)
        {
            _userService.DeclineFriendRequest(friendID);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AcceptFriendRequest(string friendID)
        {
            await _userService.AcceptFriendRequestAsync(friendID);

            return RedirectToAction("Index");
        }
    }
}

