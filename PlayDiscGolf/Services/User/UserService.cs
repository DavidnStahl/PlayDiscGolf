using Microsoft.AspNetCore.Http;
using PlayDiscGolf.Data.Cards.Scores;
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
        private readonly IScoreCardRepository _scoreCardRepository;

        public UserService(IAccountService accountService, IScoreCardRepository scoreCardRepository)
        {
            _accountService = accountService;
            _scoreCardRepository = scoreCardRepository;
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

        public Task ClaimGamesFromUsernameAsync(UserInformationViewModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<UserInformationViewModel> GetSearchResultFromQueryAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task GetScoreCardsToClaimFromUser()
        {
            throw new NotImplementedException();
        }
    }
}
