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

        public IActionResult Index()
        {
            var model = _mapper.Map<FriendViewModel>(_userService.GetFriendRequests());

            return View(model);
        }

        public IActionResult DeclineFriendRequest(string friendID)
        {
            _userService.DeclineFriendRequest(friendID);
            var model = _mapper.Map<FriendViewModel>(_userService.GetFriendRequests());

            return View(model);
        }

        public IActionResult AcceptFriendRequest(string friendID)
        {
            _userService.AcceptFriendRequest(friendID);
            var model = _mapper.Map<FriendViewModel>(_userService.GetFriendRequests());

            return View(model);
        }
        
    }
}

