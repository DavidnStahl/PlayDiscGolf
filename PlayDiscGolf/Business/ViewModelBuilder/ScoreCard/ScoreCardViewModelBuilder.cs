using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Business.ViewModelBuilder.HoleCard;
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
        public ScoreCardViewModel BuildScoreCardCreateInformation(string courseID)
        {
            var scoreCardID = Guid.NewGuid();

            var playerCardID = Guid.NewGuid();

            return new ScoreCardViewModel
            {
                CourseID = Guid.Parse(courseID),
                UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                ScoreCardID = scoreCardID,
                StartDate = DateTime.Now,
                PlayerCards = new List<PlayerCardViewModel> {
                    new PlayerCardViewModel {
                        UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                        UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                        PlayerCardID = playerCardID,
                        ScoreCardID =  scoreCardID,
                        HoleCards = _holeCardViewModelBuilder.BuildHoleCardsForCourse(Guid.Parse(courseID), playerCardID)
                    }}
            };
        }

        public ScoreCardViewModel BuildUpdatedScoreCardWithUpdatedPlayers(ScoreCardViewModel sessionModel, string newName)
        {
            var playerCardID = Guid.NewGuid();

            sessionModel.PlayerCards = (sessionModel.PlayerCards as IEnumerable<PlayerCardViewModel>)
                .Where(player => player.UserName != newName).Append(new PlayerCardViewModel
                {
                    UserName = newName,
                    ScoreCardID = sessionModel.ScoreCardID,
                    PlayerCardID = playerCardID,
                    HoleCards = _holeCardViewModelBuilder.BuildHoleCardsForCourse(sessionModel.CourseID, playerCardID)
                }).ToList();

            return sessionModel;
        }
    }
}
