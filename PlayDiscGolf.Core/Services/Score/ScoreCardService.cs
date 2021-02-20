using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Core.Business.DtoBuilder.ScoreCard;
using PlayDiscGolf.Core.Business.Session;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Core.Enums;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.Score
{
    public class ScoreCardService : IScoreCardService
    {
        private readonly IMapper _mapper;
        private readonly IScoreCardDtoBuilder _scoreCardDtoBuilder;
        private readonly string _sessionKey;
        private readonly ISessionStorage<ScoreCardDto> _sessionStorage;
        private readonly IUnitOfwork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ScoreCardService(
            ISessionStorage<ScoreCardDto> sessionStorage,
            IMapper mapper,
            IScoreCardDtoBuilder scoreCardDtoBuilder,
            IUnitOfwork unitOfWork,
            IHttpContextAccessor accessor,
            UserManager<IdentityUser> userManager)
        {
            _sessionStorage = sessionStorage;
            _mapper = mapper;
            _scoreCardDtoBuilder = scoreCardDtoBuilder;
            _sessionKey = EnumHelper.ScoreCardViewModelSessionKey.ScoreCardViewModel.ToString();
            _unitOfWork = unitOfWork;
            _httpContext = accessor;
            _userManager = userManager;
        }

        public string GetCourseName(Guid courseID)
        {
            return _unitOfWork.Courses.FindById(courseID).Name;
        }
        public ScoreCardDto GetScoreCardCreateInformation(string courseID)
        {
            var scoreCardID = Guid.NewGuid();
            var playerCardID = Guid.NewGuid();
            var holeCardDtos = new List<HoleCardDto>();
            var holes = _unitOfWork.Holes.FindAllBy(x => x.CourseID == Guid.Parse(courseID));

            for (int i = 0; i < holes.Count; i++)
                holeCardDtos.Add(new HoleCardDto
                {
                    HoleCardID = Guid.NewGuid(),
                    HoleNumber = i + 1,
                    PlayerCardID = playerCardID,
                    Score = 0
                });


            var ScoreCardDto = new ScoreCardDto
            {
                CourseID = Guid.Parse(courseID),
                UserName = _httpContext.HttpContext.User.Identity.Name,
                UserID = _userManager.GetUserId(_httpContext.HttpContext.User),
                ScoreCardID = scoreCardID,
                StartDate = DateTime.Now,
                PlayerCards = new List<PlayerCardDto> {
                    new PlayerCardDto {
                        UserID = _userManager.GetUserId(_httpContext.HttpContext.User),
                        UserName = _httpContext.HttpContext.User.Identity.Name,
                        PlayerCardID = playerCardID,
                        ScoreCardID =  scoreCardID,
                        HoleCards = holeCardDtos
                    }}
            };

            _sessionStorage.Save(_sessionKey, ScoreCardDto);

            return _sessionStorage.Get(_sessionKey);
        }

        public async Task<List<PlayerCardDto>> AddPlayerToSessionAndReturnUpdatedPlayersAsync(string newName)
        {
            var sessionModel = await _scoreCardDtoBuilder.BuildUpdatedScoreCardWithUpdatedPlayersAsync(_sessionStorage.Get(_sessionKey), newName);

            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        public List<PlayerCardDto> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer)
        {
            var sessionModel = _sessionStorage.Get(_sessionKey);
            sessionModel.PlayerCards = sessionModel.PlayerCards.Where(player => player.UserName != removePlayer).ToList();
            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        private ScoreCardDto OrderPlayercards(ScoreCardDto scoreCard)
        {
            var owner = scoreCard.PlayerCards.SingleOrDefault(x => x.UserName == scoreCard.UserName);
            var orderedPlayercards = scoreCard.PlayerCards.OrderBy(x => x.UserID).ToList();
            scoreCard.PlayerCards = orderedPlayercards;
            scoreCard.PlayerCards.Remove(owner);
            scoreCard.PlayerCards.Insert(0, owner);

            return scoreCard;
        }

        public ScoreCardGameOnDto StartGame()
        {
            var scoreCard = _mapper.Map<ScoreCardDto>(CreateScoreCard());

            if (scoreCard.PlayerCards != null)
            {
                scoreCard = OrderPlayercards(scoreCard);

            }

            return new ScoreCardGameOnDto
            {
                Hole = _mapper.Map<HoleDto>(_unitOfWork.Holes.GetCourseHole(scoreCard.CourseID, 1)),
                ScoreCard = scoreCard
            };
        }
        private ScoreCard CreateScoreCard()
        {
            var cachedScoreCard = _sessionStorage.Get(_sessionKey);
            var scoreCard = _mapper.Map<ScoreCard>(cachedScoreCard);
            _unitOfWork.ScoreCards.Add(scoreCard);
            _unitOfWork.Complete();

            return scoreCard;
        }

        public ScoreCardGameOnDto UpdateScore(string scoreCardID, string holeNumber, string addOrRemove, string userName)
        {
            var scoreCard = _unitOfWork.ScoreCards.GetSingleScoreCardAndIncludePlayerCardAndHoleCardBy(x => x.ScoreCardID == Guid.Parse(scoreCardID));
            var hole = _unitOfWork.Holes.GetCourseHole(scoreCard.CourseID, Convert.ToInt32(holeNumber));

            if (addOrRemove != null)
                UpdateScoreCard(userName, holeNumber, hole, addOrRemove, scoreCard);

            var scoreCardDto = _mapper.Map<ScoreCardDto>(scoreCard);
            scoreCardDto = OrderPlayercards(scoreCardDto);

            return new ScoreCardGameOnDto 
            {
                Hole = _mapper.Map<HoleDto>(hole),
                ScoreCard = scoreCardDto
            };
        }

        private void UpdateScoreCard(string userName, string holeNumber, Hole hole, string addOrRemove, ScoreCard scoreCard)
        {
            var playerCard = scoreCard.PlayerCards.SingleOrDefault(x => x.UserName == userName);
            var holeCard = playerCard.HoleCards.Where(x => x.HoleNumber == Convert.ToInt32(holeNumber)).ToList();

            if (addOrRemove == EnumHelper.PlusAndMinus.Plus.ToString()) IncreaseScoreOnHoleCard(holeCard, scoreCard, playerCard, hole);
            else DecreaseScoreOnHoleCard(holeCard, scoreCard, playerCard, hole);
        }

        private void UpdatePlayerTotalScore(ScoreCard scoreCard, PlayerCard playerCard)
        {
            var playerTotalThrowsFromStartedHoles = playerCard.HoleCards
                .Where(x => x.Score > 0)
                .Select(x => x.Score)
                .Sum();

            var totalParValueFromStartedHoles = _unitOfWork.Courses.GetCourseByIDAndIncludeHoles(scoreCard.CourseID).Holes
                .Where(hole => playerCard.HoleCards.
                              Where(r => r.Score > 0)
                              .Select(x => x.HoleNumber)
                              .Contains(hole.HoleNumber))
                .Select(hole => hole.ParValue)
                .Sum();


            playerCard.TotalScore = playerTotalThrowsFromStartedHoles - totalParValueFromStartedHoles;
            _unitOfWork.PlayerCards.Edit(playerCard);
            _unitOfWork.Complete();
        }

        private void IncreaseScoreOnHoleCard(IEnumerable<HoleCard> holeCard, ScoreCard scoreCard, PlayerCard playerCard, Hole hole)
        {
            if (holeCard.SingleOrDefault().Score == 0)
            {
                var item = holeCard.SingleOrDefault();
                item.Score++;
                _unitOfWork.HoleCards.Edit(item);
            }
            else
            {
                var item = holeCard.SingleOrDefault();
                item.Score++;
                _unitOfWork.HoleCards.Edit(item);
            }

            _unitOfWork.Complete();
            UpdatePlayerTotalScore(scoreCard, playerCard);
        }

        private void DecreaseScoreOnHoleCard(IEnumerable<HoleCard> holeCard, ScoreCard scoreCard, PlayerCard playerCard, Hole hole)
        {
            if (holeCard.SingleOrDefault().Score != 0)
            {
                var item = holeCard.SingleOrDefault();
                item.Score--;
                _unitOfWork.HoleCards.Edit(item);
                _unitOfWork.Complete();
                UpdatePlayerTotalScore(scoreCard, playerCard);
            }   
        }

        public ScoreCardGameOnDto OpenScoreCard(string scoreCardID)
        {
            var scoreCard = _unitOfWork.ScoreCards.GetSingleScoreCardAndIncludePlayerCardAndHoleCardBy(x => x.ScoreCardID == Guid.Parse(scoreCardID));
            var hole = _unitOfWork.Holes.GetCourseHole(scoreCard.CourseID, 1);

            var scoreCardDto = _mapper.Map<ScoreCardDto>(scoreCard);
            scoreCardDto = OrderPlayercards(scoreCardDto);

            var model = new ScoreCardGameOnDto
            {
                Hole = _mapper.Map<HoleDto>(hole),
                ScoreCard = scoreCardDto
            };

            return model;
        }
    }
}
