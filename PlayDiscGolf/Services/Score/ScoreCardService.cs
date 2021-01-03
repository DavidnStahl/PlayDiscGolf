using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlayDiscGolf.Business.Session;
using PlayDiscGolf.Business.ViewModelBuilder.HoleCard;
using PlayDiscGolf.Business.ViewModelBuilder.ScoreCard;
using PlayDiscGolf.Data;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Score
{
    public class ScoreCardService : IScoreCardService
    {
        private readonly ISessionStorage<ScoreCardViewModel> _sessionStorage;
        private readonly IEntityRepository<ScoreCard> _scoreCardRepository;
        private readonly IEntityRepository<PlayerCard> _playerCardRepository;
        private readonly IEntityRepository<HoleCard> _holeCardRepository;
        private readonly IEntityRepository<Hole> _holeRepository;
        private readonly IMapper _mapper;
        private readonly IScoreCardViewModelBuilder _scoreCardViewModelBuilder;
        private readonly string _sessionKey;

        public ScoreCardService(
            ISessionStorage<ScoreCardViewModel> sessionStorage,
            IMapper mapper,
            IScoreCardViewModelBuilder scoreCardViewModelBuilder,
            IEntityRepository<ScoreCard> scoreCardRepository,
            IEntityRepository<PlayerCard> playerCardRepository,
            IEntityRepository<HoleCard> holeCardRepository,
            IEntityRepository<Hole> holeRepository)
        {
            _sessionStorage = sessionStorage;
            _scoreCardRepository = scoreCardRepository;
            _playerCardRepository = playerCardRepository;
            _holeCardRepository = holeCardRepository;
            _holeRepository = holeRepository;
            _mapper = mapper;
            _scoreCardViewModelBuilder = scoreCardViewModelBuilder;
            _sessionKey = EnumHelper.ScoreCardViewModelSessionKey.ScoreCardViewModel.ToString();
        }
        public ScoreCardViewModel GetScoreCardCreateInformation(string courseID)
        {
            _sessionStorage.Save(_sessionKey, _scoreCardViewModelBuilder.BuildScoreCardCreateInformation(courseID));

            return _sessionStorage.Get(_sessionKey);
        }

        public List<PlayerCardViewModel> AddPlayerToSessionAndReturnUpdatedPlayers(string newName)
        {
            var sessionModel = _scoreCardViewModelBuilder.BuildUpdatedScoreCardWithUpdatedPlayers(_sessionStorage.Get(_sessionKey), newName);

            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        public List<PlayerCardViewModel> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer)
        {
            var sessionModel = _sessionStorage.Get(_sessionKey);

            sessionModel.PlayerCards = sessionModel.PlayerCards.Where(player => player.UserName != removePlayer).ToList();

            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        public ScoreCardGameOnViewModel StartGame()
        {
            var scoreCard = CreateScoreCard();

            return new ScoreCardGameOnViewModel
            {
                Hole = _holeRepository.FindBy(x => x.CourseID == scoreCard.CourseID && x.HoleNumber == 1).FirstOrDefault(),
                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };
        }

        private ScoreCard CreateScoreCard()
        {
            var cachedScoreCard = _sessionStorage.Get(_sessionKey);
            var scoreCard = _mapper.Map<ScoreCard>(cachedScoreCard);
            _scoreCardRepository.Add(scoreCard);
            _scoreCardRepository.Save();

            return scoreCard;
        }

        public ScoreCardGameOnViewModel UpdateScore(string scoreCardID, string holeNumber, string addOrRemove, string userName)
        {
            var scoreCard = _scoreCardRepository
                .GetAll()
                .Include(x => x.PlayerCards)
                .ThenInclude(x => x.HoleCards)
                .Where(x => x.ScoreCardID == Guid.Parse(scoreCardID));

            var hole = _holeRepository
                .FindBy(x => x.CourseID == scoreCard.Select(x => x.CourseID).SingleOrDefault() && x.HoleNumber == Convert.ToInt32(holeNumber))
                .FirstOrDefault();

            if (addOrRemove != null)
                UpdateScoreCard(userName, holeNumber, hole, addOrRemove, scoreCard);
       
            return new ScoreCardGameOnViewModel 
            {
                Hole = hole,
                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard.SingleOrDefault())
            };
        }

        private void UpdateScoreCard(string userName, string holeNumber, Hole hole, string addOrRemove, IQueryable<ScoreCard> scoreCard)
        {
            var playerCard = scoreCard.SelectMany(x => x.PlayerCards).Where(x => x.UserName == userName);
            var holeCard = playerCard.SelectMany(x => x.HoleCards).Where(x => x.HoleNumber == Convert.ToInt32(holeNumber));

            if (addOrRemove == EnumHelper.PlusAndMinus.Plus.ToString()) IncreaseScoreOnHoleCard(holeCard, scoreCard.SingleOrDefault(), playerCard, hole);
            else DecreaseScoreOnHoleCard(holeCard, scoreCard.SingleOrDefault(), playerCard, hole);
        }

        private void UpdatePlayerTotalScore(IQueryable<HoleCard> holeCard, ScoreCard scoreCard, IQueryable<PlayerCard> playerCard)
        {
            var playerTotalThrowsFromStartedHoles = playerCard
                .SelectMany(x => x.HoleCards)
                .Where(x => x.Score > 0)
                .Select(x => x.Score)
                .Sum();

            var totalParValueFromStartedHoles = _holeRepository
                .FindBy(x => x.CourseID == scoreCard.CourseID)
                .Where(x => holeCard
                            .Where(y => y.Score > 0)
                            .Select(y => y.HoleNumber)
                            .ToList()
                            .Contains(x.HoleNumber))
                .Select(x => x.ParValue).Sum();

            var playerItem = playerCard.SingleOrDefault();

            playerItem.TotalScore = playerTotalThrowsFromStartedHoles - totalParValueFromStartedHoles;

            _playerCardRepository.Edit(playerItem);
            _playerCardRepository.Save();
        }

        private void IncreaseScoreOnHoleCard(IQueryable<HoleCard> holeCard, ScoreCard scoreCard, IQueryable<PlayerCard> playerCard, Hole hole)
        {
            if (holeCard.SingleOrDefault().Score == 0)
            {
                var item = holeCard.SingleOrDefault();
                item.Score = hole.ParValue + 1;
                _holeCardRepository.Edit(item);
            }
            else
            {
                var item = holeCard.SingleOrDefault();
                item.Score++;
                _holeCardRepository.Edit(item);
            }

            _holeCardRepository.Save();
            UpdatePlayerTotalScore(holeCard, scoreCard, playerCard);
        }

        private void DecreaseScoreOnHoleCard(IQueryable<HoleCard> holeCard, ScoreCard scoreCard, IQueryable<PlayerCard> playerCard, Hole hole)
        {
            if (holeCard.SingleOrDefault().Score == 0)
            {
                var item = holeCard.SingleOrDefault();
                item.Score = hole.ParValue - 1;
                _holeCardRepository.Edit(item);
            }
            else
            {
                var item = holeCard.SingleOrDefault();
                item.Score--;
                _holeCardRepository.Edit(item);
            }

            _holeCardRepository.Save();
            UpdatePlayerTotalScore(holeCard, scoreCard, playerCard);
        }

        public ScoreCardGameOnViewModel OpenScoreCard(string scoreCardID)
        {
            var scoreCard = _scoreCardRepository
                .GetAll()
                .Include(x => x.PlayerCards)
                .ThenInclude(x => x.HoleCards)
                .SingleOrDefault(x => x.ScoreCardID == Guid.Parse(scoreCardID));

            var hole = _holeRepository.FindBy(x => x.CourseID == scoreCard.CourseID && x.HoleNumber == 1).FirstOrDefault();

            var model =  new ScoreCardGameOnViewModel
            {
                Hole = hole,
                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };

            return model;
        }
    }
}
