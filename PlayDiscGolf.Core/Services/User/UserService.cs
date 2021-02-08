using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
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
            var friends = _unitOfWork.Friends.FindBy(x => x.UserID == Guid.Parse(inloggedUserID)).ToList();

            return _mapper.Map<List<FriendDto>>(friends);
        }

        /*public async Task<UserInformationDto> GetUserInformationAsync()
        {
            var inloggedUserID = await _accountService.GetInloggedUserIDAsync();

            return new UserInformationDto
            {
                UserID = inloggedUserID,
                Email = await _accountService.GetEmailAsync(),
                Username = _accountService.GetUserName(),
                Friends = _unitOfWork.Friends.FindBy(x => x.UserID == Guid.Parse(inloggedUserID)).Select(x => x.UserName).ToList()
            };
        }*/

        public async Task RemoveFriendAsync(string username)
        {
            var inloggedUser = await _accountService.GetUserByQueryAsync(username);
            var friend = _unitOfWork.Friends.FindBy(x => x.UserID == Guid.Parse(inloggedUser.Id) && x.UserName == username).SingleOrDefault();

            if(friend.FriendRequestAccepted == true)
            {
                var inloggedUserAsFriend = _unitOfWork.Friends.FindBy(x => x.UserID == friend.FriendUserID && x.UserName == inloggedUser.UserName).SingleOrDefault();
                _unitOfWork.Friends.Delete(inloggedUserAsFriend);
            }

            _unitOfWork.Friends.Delete(friend);
            _unitOfWork.Complete();
        }

        public async Task<List<FriendDto>> GetFriendRequests()
        {
            var username = _accountService.GetUserName();
            var user = await _accountService.GetUserByQueryAsync(username);

            return _mapper.Map<List<FriendDto>>(_unitOfWork.Friends.FindBy(x => x.FriendUserID == Guid.Parse(user.Id) && x.FriendRequestAccepted == false).ToList());
        }

        public void AcceptFriendRequest(string friendID)
        {
            var friend = _unitOfWork.Friends.FindById(Guid.Parse(friendID));

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
                .FindBy(x => x.UserID == Guid.Parse(inloggedUserID) && x.UserName == user.NormalizedUserName).SingleOrDefault() != null 
                ? null 
                : user.NormalizedUserName;
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
