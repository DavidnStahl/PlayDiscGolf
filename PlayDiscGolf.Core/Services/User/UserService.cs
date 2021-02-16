using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IUnitOfwork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IUnitOfwork unitOfWork, IMapper mapper)
        {
            _accountService = accountService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<FriendDto>> GetFriendsAsync()
        {
            var inloggedUserID = await _accountService.GetInloggedUserIDAsync();
            var friends = _unitOfWork.Friends.FindAllBy(x => x.UserID == Guid.Parse(inloggedUserID));

            return _mapper.Map<List<FriendDto>>(friends);
        }

        public async Task RemoveFriendAsync(string friendID)
        {
            var inloggedUsername = _accountService.GetUserName();
            var friend = _unitOfWork.Friends.FindSingleBy(x => x.FriendID == Guid.Parse(friendID));

            if(friend.FriendRequestAccepted == true)
            {
               await RemoveExtraFriendAsync(friend.FriendUserID);
            }

            _unitOfWork.Friends.Delete(friend);
            _unitOfWork.Complete();
        }

        public async Task<List<FriendDto>> GetFriendRequestsAsync()
        {
            var username = _accountService.GetUserName();
            var user = await _accountService.GetUserByQueryAsync(username);
            var friendRequests = _unitOfWork.Friends.FindAllBy(x => x.FriendUserID == Guid.Parse(user.Id) && x.FriendRequestAccepted == false);

            var friendDto = new List<FriendDto>();

            foreach (var request in friendRequests)
            {
                var requestedUsername = await _accountService.GetUserByIDAsync(request.UserID.ToString());
                request.UserName = requestedUsername.UserName;

                friendDto.Add(_mapper.Map<FriendDto>(request));
            }

            return friendDto;
        }

        public async Task RemoveExtraFriendAsync(Guid userRemovedFriendUserID)
        {
            var inloggedUsername = _accountService.GetUserName();
            var inloggedUser = await _accountService.GetUserByQueryAsync(inloggedUsername);

            var friend = _unitOfWork.Friends.FindSingleBy(x => x.UserID == userRemovedFriendUserID && x.FriendUserID == Guid.Parse(inloggedUser.Id));

            _unitOfWork.Friends.Delete(friend);
        }

        private async Task AddExtraFriendAsync(string friendRequestSenderUserID)
        {
            var user = await _accountService.GetUserByIDAsync(friendRequestSenderUserID);
            var inloggedUserID = await _accountService.GetInloggedUserIDAsync();
            var inloggedUser = await _accountService.GetUserByIDAsync(inloggedUserID);

            var friend = new Friend
            {
                UserName = user.UserName,
                UserID = Guid.Parse(inloggedUser.Id),
                FriendID = Guid.NewGuid(),
                FriendUserID = Guid.Parse(user.Id),
                FriendRequestAccepted = true
            };

            _unitOfWork.Friends.Add(friend);
            _unitOfWork.Complete();
        }

        public async Task AcceptFriendRequestAsync(string friendID)
        {
            var friend = _unitOfWork.Friends.FindById(Guid.Parse(friendID));
            friend.FriendRequestAccepted = true;

            _unitOfWork.Friends.Edit(friend);

            await AddExtraFriendAsync(friend.UserID.ToString());
        }

        public void DeclineFriendRequest(string friendID)
        {
            var friend = _unitOfWork.Friends.FindById(Guid.Parse(friendID));

            _unitOfWork.Friends.Delete(friend);
            _unitOfWork.Complete();
        }

    public async Task<string> SearchUsersAsync(string query)
        {
            var user = await _accountService.GetUserByQueryAsync(query);
            var inloggedUserID = await _accountService.GetInloggedUserIDAsync();

            if (user.Id == inloggedUserID)
                return null;

            return _unitOfWork.Friends
                .FindSingleBy(x => x.UserID == Guid.Parse(inloggedUserID) && x.UserName.ToLower() == user.NormalizedUserName.ToLower()) != null 
                ? null 
                : user.UserName;
        }

        public async Task SendFriendRequestAsync(string username)
        {
            var user = await _accountService.GetUserByQueryAsync(username);
            var inloggedUserID = await _accountService.GetInloggedUserIDAsync();

            var friend = new Friend
            {
                UserName = username,
                UserID = Guid.Parse(inloggedUserID),
                FriendID = Guid.NewGuid(),
                FriendUserID = Guid.Parse(user.Id),
                FriendRequestAccepted = false
            };

            _unitOfWork.Friends.Add(friend);
            _unitOfWork.Complete();
        }
    }
}
