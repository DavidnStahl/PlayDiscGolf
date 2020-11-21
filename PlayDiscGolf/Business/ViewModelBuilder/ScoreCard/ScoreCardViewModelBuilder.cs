using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PlayDiscGolf.Business.Session;
using PlayDiscGolf.Business.ViewModelBuilder.HoleCard;
using PlayDiscGolf.Data.Cards.Holes;
using PlayDiscGolf.Data.Cards.Players;
using PlayDiscGolf.Data.Cards.Scores;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.ViewModelBuilder.ScoreCard
{
    public class ScoreCardViewModelBuilder : IScoreCardViewModelBuilder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHoleCardViewModelBuilder _holeCardViewModelBuilder;

        public ScoreCardViewModelBuilder(UserManager<IdentityUser> userManager,
            IHoleCardViewModelBuilder holeCardViewModelBuilder, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _holeCardViewModelBuilder = holeCardViewModelBuilder;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ScoreCardViewModel> BuildScoreCardCreateInformationAsync(string courseID)
        {
            Guid scoreCardID = Guid.NewGuid();
            Guid playerCardID = Guid.NewGuid();

            return new ScoreCardViewModel
            {
                CourseID = Guid.Parse(courseID),
                UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                ScoreCardID = scoreCardID,
                PlayerCards = new List<PlayerCardViewModel> {
                    new PlayerCardViewModel {
                        UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                        UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                        PlayerCardID = playerCardID,
                        ScoreCardID =  scoreCardID,
                        HoleCards = await _holeCardViewModelBuilder.BuildHoleCardsForCourseAsync(Guid.Parse(courseID), playerCardID)
                    }}
            };
        }

        public async Task<ScoreCardViewModel> BuildUpdatedScoreCardWithUpdatedPlayersAsync(ScoreCardViewModel sessionModel, string newName)
        {
            Guid playerCardID = Guid.NewGuid();

            sessionModel.PlayerCards = (sessionModel
                .PlayerCards as IEnumerable<PlayerCardViewModel>)
                .Where(player => player.UserName != newName).Append(new PlayerCardViewModel
                {
                    UserName = newName,
                    ScoreCardID = sessionModel.ScoreCardID,
                    PlayerCardID = playerCardID,
                    HoleCards = await _holeCardViewModelBuilder.BuildHoleCardsForCourseAsync(sessionModel.CourseID, playerCardID)
                }).ToList();

            return sessionModel;
        }
    }
}
