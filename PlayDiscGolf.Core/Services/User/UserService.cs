using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.Core.Services.Account;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;

        public UserService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<UserInformationDto> GetUserInformationAsync()
        {
            return new UserInformationDto
            {
                UserID = await _accountService.GetInloggedUserIDAsync(),
                Email = await _accountService.GetEmailAsync(),
                Username = _accountService.GetUserName()
            };
        }
    }
}
