using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Core.Business.DtoBuilder.HoleCard;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Services.Account;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Business.DtoBuilder.ScoreCard
{
    public class ScoreCardDtoBuilder : IScoreCardDtoBuilder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfwork _unitOfwork;
        private readonly IAccountService _accountService;
        private readonly IHoleCardDtoBuilder _holeCardViewModelBuilder;

        public ScoreCardDtoBuilder(
            UserManager<IdentityUser> userManager,
            IHoleCardDtoBuilder holeCardViewModelBuilder,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfwork unitOfwork,
            IAccountService accountService)
        {
            _userManager = userManager;
            _holeCardViewModelBuilder = holeCardViewModelBuilder;
            _httpContextAccessor = httpContextAccessor;
            _unitOfwork = unitOfwork;
            _accountService = accountService;
        }
        public ScoreCardDto BuildScoreCardCreateInformation(string courseID)
        {
            var scoreCardID = Guid.NewGuid();

            var playerCardID = Guid.NewGuid();

            var holeCardDtos = new List<HoleCardDto>();

            var holes = _unitOfwork.Holes.FindBy(x => x.CourseID == Guid.Parse(courseID));

            for (int i = 0; i < holes.Count; i++)
                holeCardDtos.Add(new HoleCardDto
                {
                    HoleCardID = Guid.NewGuid(),
                    HoleNumber = i + 1,
                    PlayerCardID = playerCardID,
                    Score = 0
                });


            return new ScoreCardDto
            {
                CourseID = Guid.Parse(courseID),
                UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                ScoreCardID = scoreCardID,
                StartDate = DateTime.Now,
                PlayerCards = new List<PlayerCardDto> {
                    new PlayerCardDto {
                        UserID = _userManager.GetUserId(_httpContextAccessor.HttpContext.User),
                        UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                        PlayerCardID = playerCardID,
                        ScoreCardID =  scoreCardID,
                        HoleCards = holeCardDtos
                    }}
            };
        }

        public async Task<ScoreCardDto> BuildUpdatedScoreCardWithUpdatedPlayersAsync(ScoreCardDto sessionModel, string newName)
        {
            

            if (sessionModel.PlayerCards.Where(x => x.UserName == newName).SingleOrDefault() != null)
                return sessionModel;

            var inloggedUserID = await _accountService.GetInloggedUserIDAsync();

            var playerCardID = Guid.NewGuid();

            sessionModel.PlayerCards = (sessionModel.PlayerCards as IEnumerable<PlayerCardDto>)
                .Where(player => player.UserName != newName).Append(new PlayerCardDto
                {
                    UserName = newName,
                    ScoreCardID = sessionModel.ScoreCardID,
                    PlayerCardID = playerCardID,
                    HoleCards = _holeCardViewModelBuilder.BuildHoleCardsForCourse(sessionModel.CourseID, playerCardID),
                    UserID = _unitOfwork.Friends.FindBy(x => x.UserName == newName && x.UserID == Guid.Parse(inloggedUserID)).SingleOrDefault().FriendUserID.ToString()
                }).ToList();

            return sessionModel;
        }
    }
}
