using Microsoft.AspNetCore.Http;
using PlayDiscGolf.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task ClaimGamesFromUsername(UserInformationViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInformationViewModel> GetUserInformation() => 
            new UserInformationViewModel
            {
                UserID = await _accountService.GetInloggedUserIDAsync(),
                Email = await _accountService.GetEmailAsync(),
                Username = _accountService.GetUserName()
            };

        public Task SaveUserInformation(UserInformationViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
