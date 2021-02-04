using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IUnitOfwork _unitOfWork;

        public UserService(IAccountService accountService, IUnitOfwork unitOfWork)
        {
            _accountService = accountService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserInformationDto> GetUserInformationAsync()
        {
            var userID = await _accountService.GetInloggedUserIDAsync();

            return new UserInformationDto
            {
                UserID = userID,
                Email = await _accountService.GetEmailAsync(),
                Username = _accountService.GetUserName(),
                Friends = _unitOfWork.Friends.FindBy(x => x.UserID == Guid.Parse(userID)).Select(x => x.UserName).ToList()
            };
        }

        public async Task<string> SearchUsersAsync(string query)
        {
            var user = await _accountService.GetUserByQueryAsync(query);

            if (user == null)
                return null;

            return user.NormalizedUserName;
        }
    }
}
