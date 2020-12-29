using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PlayDiscGolf.Business.Session;
using PlayDiscGolf.Business.ViewModelBuilder.HoleCard;
using PlayDiscGolf.Business.ViewModelBuilder.ScoreCard;
using PlayDiscGolf.Data;
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

namespace PlayDiscGolf.Services.Score
{
    public class ScoreCardService : IScoreCardService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISessionStorage<ScoreCardViewModel> _sessionStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEntityRepository<ScoreCard> _scoreCardRepository;
        private readonly IEntityRepository<PlayerCard> _playerCardRepository;
        private readonly IEntityRepository<HoleCard> _holeCardRepository;
        private readonly IEntityRepository<Hole> _holeRepository;
        private readonly IMapper _mapper;
        private readonly IScoreCardViewModelBuilder _scoreCardViewModelBuilder;
        private readonly IHoleCardViewModelBuilder _holeCardViewModelBuilder;
        private readonly ICourseRepository _courseRepository;
        private readonly string _sessionKey;

        public ScoreCardService(UserManager<IdentityUser> userManager, ISessionStorage<ScoreCardViewModel> sessionStorage
            , IHttpContextAccessor httpContextAccessor/*, IScoreCardRepository scoreCardRepository, IPlayerCardRepository playerCardRepository,
            IHoleCardRepository holeCardRepository, IHoleRepository holeRepository*/, IMapper mapper, IScoreCardViewModelBuilder scoreCardViewModelBuilder,
            IHoleCardViewModelBuilder holeCardViewModelBuilder, ICourseRepository courseRepository,
            IEntityRepository<ScoreCard> scoreCardRepository, IEntityRepository<PlayerCard> playerCardRepository, IEntityRepository<HoleCard> holeCardRepository, IEntityRepository<Hole> holeRepository)
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
            _courseRepository = courseRepository;
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
            //await _scoreCardRepository.CreateScoreCardIncludePlayerCardAsync(_mapper.Map<Models.Models.DataModels.ScoreCard>(_sessionStorage.Get(_sessionKey)));

            var cachedScoreCard = _sessionStorage.Get(_sessionKey);

            var scorecard = _mapper.Map<ScoreCard>(cachedScoreCard);

            _scoreCardRepository.Add(scorecard); 
            _scoreCardRepository.Save();

            //await _scoreCardRepository.SaveChangesAsync();

            var scoreCard = await GetScoreCardAsync(_sessionStorage.Get(_sessionKey).ScoreCardID.ToString(), _sessionStorage.Get(_sessionKey).CourseID);

            var hole = await GetHoleAsync(scoreCard.CourseID, "1");

            return new ScoreCardGameOnViewModel
            {
                Hole = hole,
                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };
        }

        public async Task<ScoreCardGameOnViewModel> ModifyScoreCardAsync(string scoreCardID, string holeNumber, string addOrRemove, string userName, Guid courseID)
        {
            var scoreCard = await GetScoreCardAsync(scoreCardID,courseID);
            var hole = await GetHoleAsync(scoreCard.CourseID, holeNumber);

            if (addOrRemove != null) 
                await UpdateScoreCardAsync(userName, holeNumber, hole, addOrRemove, scoreCard);

            return new ScoreCardGameOnViewModel 
            {
                Hole = hole,
                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };
        }

        private async Task<Hole> GetHoleAsync(Guid courseID, string holeNumber) =>
            (await _holeRepository.GetHolesByCourseIDAsync(courseID) as IEnumerable<Hole>) .FirstOrDefault(hole => hole.HoleNumber == Convert.ToInt32(holeNumber));

        private HoleCard GetHoleCard(PlayerCard playerCard, string holeNumber) =>
            (playerCard.HoleCards as IEnumerable<HoleCard>).FirstOrDefault(hole => hole.HoleNumber == Convert.ToInt32(holeNumber));

        private PlayerCard GetPlayerCard(Models.Models.DataModels.ScoreCard scoreCard, string userName) =>
            (scoreCard.PlayerCards as IEnumerable<PlayerCard>).FirstOrDefault(playerCard => playerCard.UserName == userName);

        private async Task<Models.Models.DataModels.ScoreCard> GetScoreCardAsync(string scoreCardID, Guid courseID) =>
            (await _scoreCardRepository.GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(_userManager.GetUserId(_httpContextAccessor.HttpContext.User),courseID) 
            as IEnumerable<Models.Models.DataModels.ScoreCard>).FirstOrDefault(scoreCard => scoreCard.ScoreCardID == Guid.Parse(scoreCardID));


        private async Task UpdateScoreCardAsync(string userName, string holeNumber, Hole hole, string addOrRemove, Models.Models.DataModels.ScoreCard scoreCard)
        {
            var playerCard = GetPlayerCard(scoreCard, userName);

            var holeCard = GetHoleCard(playerCard, holeNumber);

            if (addOrRemove == EnumHelper.PlusAndMinus.Plus.ToString()) 
                await IncreaseScoreOnHoleCardAsync(holeCard, scoreCard, playerCard, hole);
            else if(addOrRemove == EnumHelper.PlusAndMinus.Minus.ToString()) 
                await DecreaseScoreOnHoleCardAsync(holeCard, scoreCard, playerCard, hole);
        }

        private async Task IncreaseScoreOnHoleCardAsync(HoleCard holeCard, Models.Models.DataModels.ScoreCard scoreCard, PlayerCard playerCard, Hole hole)
        {
            if (holeCard.Score == 0)
                holeCard.Score = hole.ParValue + 1;
            else
                holeCard.Score++;

            _scoreCardRepository.UpdateScoreCard(scoreCard);

            await _scoreCardRepository.SaveChangesAsync();

            await UpdatePlayerTotalScoreAsync(scoreCard, playerCard);

            _scoreCardRepository.UpdateScoreCard(scoreCard);

            await _scoreCardRepository.SaveChangesAsync();
        }

        private async Task<int> UpdatePlayerTotalScoreAsync(Models.Models.DataModels.ScoreCard scoreCard, PlayerCard playerCard) =>
            playerCard.TotalScore = GetPlayerTotalThrowsFromStartedHoles(playerCard) - await GetTotalParValueFromStartedHolesAsync(scoreCard, playerCard);

        private int GetPlayerTotalThrowsFromStartedHoles(PlayerCard playerCard) =>
            (playerCard.HoleCards as IEnumerable<HoleCard>).Where(holeCard => holeCard.Score > 0).Select(holeCard => holeCard.Score).Sum();

        private async Task<int> GetTotalParValueFromStartedHolesAsync(Models.Models.DataModels.ScoreCard scoreCard, PlayerCard playerCard) =>
            ((await _courseRepository.GetCourseByIDAsync(scoreCard.CourseID)).Holes as IEnumerable<Hole>).Where(course => (playerCard.HoleCards as IEnumerable<HoleCard>)
            .Where(r => r.Score > 0).Select(holeCard => holeCard.HoleNumber).ToList().Contains(course.HoleNumber)).Select(hole => hole.ParValue).Sum();

        private async Task DecreaseScoreOnHoleCardAsync(HoleCard holeCard, Models.Models.DataModels.ScoreCard scoreCard, PlayerCard playerCard, Hole hole)
        {
            if (holeCard.Score == 0)
                holeCard.Score = hole.ParValue - 1;
            else
                holeCard.Score--;

            _scoreCardRepository.UpdateScoreCard(scoreCard);

            await _scoreCardRepository.SaveChangesAsync();

            await UpdatePlayerTotalScoreAsync(scoreCard, playerCard);

            _scoreCardRepository.UpdateScoreCard(scoreCard);

            await _scoreCardRepository.SaveChangesAsync();
        }

        public async Task<ScoreCardGameOnViewModel> OpenScoreCardAsync(string scoreCardID,Guid courseID)
        {
            var scoreCard = await GetScoreCardAsync(scoreCardID, courseID);
            var hole = await GetHoleAsync(scoreCard.CourseID, "1");

            var model =  new ScoreCardGameOnViewModel
            {
                Hole = hole,
                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };

            return model;
        }
    }
}
