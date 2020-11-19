using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PlayDiscGolf.Business.Session;
using PlayDiscGolf.Enums;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.ScoreCard
{
    public class ScoreCardService : IScoreCardService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISessionStorage<ScoreCardViewModel> _sessionStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _sessionKey;

        public ScoreCardService(UserManager<IdentityUser> userManager, ISessionStorage<ScoreCardViewModel> sessionStorage
            ,IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _sessionStorage = sessionStorage;
            _httpContextAccessor = httpContextAccessor;
            _sessionKey = EnumHelper.ScoreCardViewModelSessionKey.ScoreCardViewModel.ToString();
        }
        public ScoreCardViewModel GetScoreCardCreateInformation(string courseID)
        {
            Guid scoreCardID = Guid.NewGuid();

            var model = new ScoreCardViewModel
            {
                CourseID = courseID,
                UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                ScoreCardID = scoreCardID.ToString(),
                PlayerCards = new List<PlayerCardViewModel> {
                    new PlayerCardViewModel {
                        UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                        UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                        PlayerCardID = Guid.NewGuid().ToString(),
                        ScoreCardID =  scoreCardID.ToString()}}
            };

            _sessionStorage.Save(_sessionKey, model);

            return model;
        }

        public List<PlayerCardViewModel> AddPlayerToSessionAndReturnUpdatedPlayers(string newName)
        {
            var sessionModel = _sessionStorage.Get(_sessionKey);

            sessionModel.PlayerCards = (sessionModel
                .PlayerCards as IEnumerable<PlayerCardViewModel>)
                .Where(player => player.UserName != newName).Append(new PlayerCardViewModel
                {
                    UserName = newName,
                    ScoreCardID = sessionModel.ScoreCardID,
                    PlayerCardID = Guid.NewGuid().ToString()

                }).ToList();

            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        public List<PlayerCardViewModel> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer)
        {
            var sessionModel = _sessionStorage.Get(_sessionKey);

            sessionModel.PlayerCards = (sessionModel.PlayerCards.Where(player => player.UserName != removePlayer)
                as IEnumerable<PlayerCardViewModel>).ToList();

            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        public Task ClaimScoreCardAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public Task CreateScoreCardAsync(string courseID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteScoreCardAsync(string scoreCardID)
        {
            throw new NotImplementedException();
        }

        public Task EditScoreCardAsync(string scoreCardID)
        {
            throw new NotImplementedException();
        }

        
    }
}
