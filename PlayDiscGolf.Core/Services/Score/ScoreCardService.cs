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
        public ScoreCardDto GetScoreCardCreateInformation(string courseID)
        {
            var scoreCardID = Guid.NewGuid();

            var playerCardID = Guid.NewGuid();

            var holeCardDtos = new List<HoleCardDto>();

            var holes = _unitOfWork.Holes.FindBy(x => x.CourseID == Guid.Parse(courseID));

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

        public List<PlayerCardDto> AddPlayerToSessionAndReturnUpdatedPlayers(string newName)
        {
            var sessionModel = _scoreCardDtoBuilder.BuildUpdatedScoreCardWithUpdatedPlayers(_sessionStorage.Get(_sessionKey), newName);

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

        public ScoreCardGameOnDto StartGame()
        {
            var scoreCard = CreateScoreCard();

            return new ScoreCardGameOnDto
            {
                Hole = _mapper.Map<HoleDto>(_unitOfWork.Holes.GetCourseHole(scoreCard.CourseID, 1)),
                ScoreCard = _mapper.Map<ScoreCardDto>(scoreCard)
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
            var scoreCard = _unitOfWork.ScoreCards.GetScoreCardAndIncludePlayerCardAndHoleCard(x => x.ScoreCardID == Guid.Parse(scoreCardID)).FirstOrDefault();

            var hole = _unitOfWork.Holes.GetCourseHole(scoreCard.CourseID, Convert.ToInt32(holeNumber));

            if (addOrRemove != null)
                UpdateScoreCard(userName, holeNumber, hole, addOrRemove, scoreCard);
       
            return new ScoreCardGameOnDto 
            {
                Hole = _mapper.Map<HoleDto>(hole),
                ScoreCard = _mapper.Map<ScoreCardDto>(scoreCard)
            };
        }

        private void UpdateScoreCard(string userName, string holeNumber, Hole hole, string addOrRemove, ScoreCard scoreCard)
        {
            var playerCard = scoreCard.PlayerCards.Where(x => x.UserName == userName);
            var holeCard = playerCard.SelectMany(x => x.HoleCards).Where(x => x.HoleNumber == Convert.ToInt32(holeNumber));

            if (addOrRemove == EnumHelper.PlusAndMinus.Plus.ToString()) IncreaseScoreOnHoleCard(holeCard, scoreCard, playerCard, hole);
            else DecreaseScoreOnHoleCard(holeCard, scoreCard, playerCard, hole);
        }

        private void UpdatePlayerTotalScore(IEnumerable<HoleCard> holeCard, ScoreCard scoreCard, IEnumerable<PlayerCard> playerCard)
        {
            var playerTotalThrowsFromStartedHoles = playerCard
                .SelectMany(x => x.HoleCards)
                .Where(x => x.Score > 0)
                .Select(x => x.Score)
                .Sum();

            var totalParValueFromStartedHoles = _unitOfWork.Holes.FindBy(x => x.CourseID == scoreCard.CourseID)
                .Where(x => holeCard
                            .Where(y => y.Score > 0)
                            .Select(y => y.HoleNumber)
                            .Contains(x.HoleNumber))
                .Select(x => x.ParValue).Sum();

            var playerItem = playerCard.SingleOrDefault();

            playerItem.TotalScore = playerTotalThrowsFromStartedHoles - totalParValueFromStartedHoles;

            _unitOfWork.PlayerCards.Edit(playerItem);
            _unitOfWork.Complete();
        }

        private void IncreaseScoreOnHoleCard(IEnumerable<HoleCard> holeCard, ScoreCard scoreCard, IEnumerable<PlayerCard> playerCard, Hole hole)
        {
            if (holeCard.SingleOrDefault().Score == 0)
            {
                var item = holeCard.SingleOrDefault();
                item.Score = hole.ParValue + 1;
                _unitOfWork.HoleCards.Edit(item);
            }
            else
            {
                var item = holeCard.SingleOrDefault();
                item.Score++;
                _unitOfWork.HoleCards.Edit(item);
            }

            _unitOfWork.Complete();
            UpdatePlayerTotalScore(holeCard, scoreCard, playerCard);
        }

        private void DecreaseScoreOnHoleCard(IEnumerable<HoleCard> holeCard, ScoreCard scoreCard, IEnumerable<PlayerCard> playerCard, Hole hole)
        {
            if (holeCard.SingleOrDefault().Score == 0)
            {
                var item = holeCard.SingleOrDefault();
                item.Score = hole.ParValue - 1;
                _unitOfWork.HoleCards.Edit(item);
            }
            else
            {
                var item = holeCard.SingleOrDefault();
                item.Score--;
                _unitOfWork.HoleCards.Edit(item);
            }

            _unitOfWork.Complete();
            UpdatePlayerTotalScore(holeCard, scoreCard, playerCard);
        }

        public ScoreCardGameOnDto OpenScoreCard(string scoreCardID)
        {
            var scoreCard = _unitOfWork.ScoreCards.GetScoreCardAndIncludePlayerCardAndHoleCard(x => x.ScoreCardID == Guid.Parse(scoreCardID)).FirstOrDefault();

            var hole = _unitOfWork.Holes.GetCourseHole(scoreCard.CourseID, 1);

            var model =  new ScoreCardGameOnDto
            {
                Hole = _mapper.Map<HoleDto>(hole),
                ScoreCard = _mapper.Map<ScoreCardDto>(scoreCard)
            };

            return model;
        }
    }
}
