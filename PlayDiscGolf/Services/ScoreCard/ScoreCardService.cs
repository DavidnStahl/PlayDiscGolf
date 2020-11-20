using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PlayDiscGolf.Business.Session;
using PlayDiscGolf.Business.ViewModelBuilder.HoleCard;
using PlayDiscGolf.Business.ViewModelBuilder.ScoreCard;
using PlayDiscGolf.Data.Cards.Holes;
using PlayDiscGolf.Data.Cards.Players;
using PlayDiscGolf.Data.Cards.Scores;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.Paging;
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
        private readonly IScoreCardRepository _scoreCardRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly IHoleCardRepository _holeCardRepository;
        private readonly IHoleRepository _holeRepository;
        private readonly IMapper _mapper;
        private readonly IScoreCardViewModelBuilder _scoreCardViewModelBuilder;
        private readonly IHoleCardViewModelBuilder _holeCardViewModelBuilder;
        private readonly string _sessionKey;

        public ScoreCardService(UserManager<IdentityUser> userManager, ISessionStorage<ScoreCardViewModel> sessionStorage
            , IHttpContextAccessor httpContextAccessor, IScoreCardRepository scoreCardRepository, IPlayerCardRepository playerCardRepository,
            IHoleCardRepository holeCardRepository, IHoleRepository holeRepository, IMapper mapper, IScoreCardViewModelBuilder scoreCardViewModelBuilder,
            IHoleCardViewModelBuilder holeCardViewModelBuilder)
        {
            _userManager = userManager;
            _sessionStorage = sessionStorage;
            _httpContextAccessor = httpContextAccessor;
            _scoreCardRepository = scoreCardRepository;
            _playerCardRepository = playerCardRepository;
            _holeCardRepository = holeCardRepository;
            _holeRepository = holeRepository;
            _mapper = mapper;
            _scoreCardViewModelBuilder = scoreCardViewModelBuilder;
            _holeCardViewModelBuilder = holeCardViewModelBuilder;
            _sessionKey = EnumHelper.ScoreCardViewModelSessionKey.ScoreCardViewModel.ToString();
        }
        public async Task<ScoreCardViewModel> GetScoreCardCreateInformationAsync(string courseID)
        {
            _sessionStorage.Save(_sessionKey, await _scoreCardViewModelBuilder.BuildScoreCardCreateInformationAsync(courseID));
            return _sessionStorage.Get(_sessionKey);
        }

        public async Task<List<PlayerCardViewModel>> AddPlayerToSessionAndReturnUpdatedPlayersAsync(string newName)
        {
            var sessionModel = await _scoreCardViewModelBuilder.BuildUpdatedScoreCardWithUpdatedPlayersAsync(_sessionStorage.Get(_sessionKey), newName);

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

        public async Task<ScoreCardGameOnViewModel> StartScoreCardAsync()
        {
            await _scoreCardRepository.CreateScoreCardIncludePlayerCardAsync(_mapper.Map<Models.Models.DataModels.ScoreCard>(_sessionStorage.Get(_sessionKey)));
            await _scoreCardRepository.SaveChangesAsync();

            var scoreCard = await GetScoreCardAsync(_sessionStorage.Get(_sessionKey).ScoreCardID.ToString());

            return new ScoreCardGameOnViewModel
            {
                Hole = (await _holeRepository.GetHolesByCourseIDAsync(scoreCard.CourseID) as IEnumerable<Hole>)
                .FirstOrDefault(hole => hole.HoleNumber == 1),

                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };
        }

        public async Task<ScoreCardGameOnViewModel> UpdateScoreCardAsync(string scoreCardID, string holeNumber, string addOrRemove, string userName)
        {
            Models.Models.DataModels.ScoreCard scoreCard = await GetScoreCardAsync(scoreCardID);

            PlayerCard playerCard = GetPlayerCard(scoreCard, userName);

            HoleCard holeCard = GetHoleCard(playerCard, holeNumber);

            Hole hole = await GetHoleAsync(scoreCard.CourseID, holeNumber);

            await UpdateScoreCardAsync(playerCard, holeCard, hole, addOrRemove, scoreCard);

            return new ScoreCardGameOnViewModel
            {
                Hole = hole,

                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(await GetScoreCardAsync(scoreCardID))
            };
        }

        private async Task<Hole> GetHoleAsync(Guid courseID, string holeNumber) =>
            (await _holeRepository.GetHolesByCourseIDAsync(courseID) as IEnumerable<Hole>)
                .FirstOrDefault(hole => hole.HoleNumber == Convert.ToInt32(holeNumber));

        private HoleCard GetHoleCard(PlayerCard playerCard, string holeNumber) =>
            (playerCard.HoleCards as IEnumerable<HoleCard>)
                .FirstOrDefault(hole => hole.HoleNumber == Convert.ToInt32(holeNumber));

        private PlayerCard GetPlayerCard(Models.Models.DataModels.ScoreCard scoreCard, string userName) =>
            (scoreCard.PlayerCards as IEnumerable<PlayerCard>)
                .FirstOrDefault(playerCard => playerCard.UserName == userName);

        private async Task<Models.Models.DataModels.ScoreCard> GetScoreCardAsync(string scoreCardID) =>
            (await _scoreCardRepository.GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(_userManager.GetUserId(_httpContextAccessor.HttpContext.User))
                as IEnumerable<Models.Models.DataModels.ScoreCard>).FirstOrDefault(scoreCard => scoreCard.ScoreCardID == Guid.Parse(scoreCardID));


        private async Task UpdateScoreCardAsync(PlayerCard playerCard, HoleCard holeCard, Hole hole, string addOrRemove, Models.Models.DataModels.ScoreCard scoreCard)
        {

            if (addOrRemove == EnumHelper.PlusAndMinus.Plus.ToString())
            {
                holeCard.Score++;

                playerCard.TotalScore = holeCard.Score - hole.ParValue;

                _scoreCardRepository.UpdateScoreCard(scoreCard);
                await _scoreCardRepository.SaveChangesAsync();
            }
            else if (addOrRemove == EnumHelper.PlusAndMinus.Minus.ToString() && holeCard.Score > 0)
            {
                holeCard.Score--;

                playerCard.TotalScore = holeCard.Score - hole.ParValue;

                _scoreCardRepository.UpdateScoreCard(scoreCard);
                await _scoreCardRepository.SaveChangesAsync();
            }
        }
    }
}
