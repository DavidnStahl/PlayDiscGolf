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
            var username = _accountService.GetUserName();
            var friends = _unitOfWork.Friends.FindBy(x => x.UserID == Guid.Parse(inloggedUserID) || (x.UserName.ToLower() == username && x.FriendRequestAccepted == true)).ToList();

            var remappedFriends = new List<Friend>();

            foreach (var friend in friends)
            {
                if(friend.UserID != Guid.Parse(inloggedUserID))
                {
                    var user = await _accountService.GetUserByID(friend.UserID.ToString());
                    friend.UserName = user.UserName;
                }


                remappedFriends.Add(friend);
            }

            return _mapper.Map<List<FriendDto>>(remappedFriends);
        }

        public async Task RemoveFriendAsync(string friendID)
        {
            var inloggedUsername = _accountService.GetUserName();
            var inloggedUser = await _accountService.GetUserByQueryAsync(inloggedUsername);
            var friend = _unitOfWork.Friends.FindBy(x => x.FriendID == Guid.Parse(friendID)).SingleOrDefault();

            _unitOfWork.Friends.Delete(friend);
            _unitOfWork.Complete();
        }

        public async Task<List<FriendDto>> GetFriendRequestsAsync()
        {
            var username = _accountService.GetUserName();
            var user = await _accountService.GetUserByQueryAsync(username);

            var friendRequests = _unitOfWork.Friends.FindBy(x => x.FriendUserID == Guid.Parse(user.Id) && x.FriendRequestAccepted == false).ToList();

            var friendDto = new List<FriendDto>();

            foreach (var request in friendRequests)
            {
                var requestedUsername = await _accountService.GetUserByID(request.UserID.ToString());
                request.UserName = requestedUsername.UserName;

                friendDto.Add(_mapper.Map<FriendDto>(request));
            }

            return friendDto;
        }

        public void AcceptFriendRequest(string friendID)
        {
            var friend = _unitOfWork.Friends.FindById(Guid.Parse(friendID));
            friend.FriendRequestAccepted = true;

            _unitOfWork.Friends.Edit(friend);
            _unitOfWork.Complete();
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
                .FindBy(x => x.UserID == Guid.Parse(inloggedUserID) && x.UserName.ToLower() == user.NormalizedUserName.ToLower()).SingleOrDefault() != null 
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
