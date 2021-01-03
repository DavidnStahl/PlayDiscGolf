using PlayDiscGolf.ViewModels.User;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;

        public UserService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<UserInformationViewModel> GetUserInformationAsync()
        {
            return new UserInformationViewModel
            {
                UserID = await _accountService.GetInloggedUserIDAsync(),
                Email = await _accountService.GetEmailAsync(),
                Username = _accountService.GetUserName()
            };
        }
    }
}
