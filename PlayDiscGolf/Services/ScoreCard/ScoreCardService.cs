using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PlayDiscGolf.Business.Session;
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
        private readonly string _sessionKey;

        public ScoreCardService(UserManager<IdentityUser> userManager, ISessionStorage<ScoreCardViewModel> sessionStorage
            ,IHttpContextAccessor httpContextAccessor, IScoreCardRepository scoreCardRepository, IPlayerCardRepository playerCardRepository,
            IHoleCardRepository holeCardRepository, IHoleRepository holeRepository, IMapper mapper)
        {
            _userManager = userManager;
            _sessionStorage = sessionStorage;
            _httpContextAccessor = httpContextAccessor;
            _scoreCardRepository = scoreCardRepository;
            _playerCardRepository = playerCardRepository;
            _holeCardRepository = holeCardRepository;
            _holeRepository = holeRepository;
            _mapper = mapper;
            _sessionKey = EnumHelper.ScoreCardViewModelSessionKey.ScoreCardViewModel.ToString();
        }
        public async Task<ScoreCardViewModel> GetScoreCardCreateInformation(string courseID)
        {
            Guid scoreCardID = Guid.NewGuid();

            Guid playerCardID = Guid.NewGuid();

            var model = new ScoreCardViewModel
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
                        HoleCards = await CreateHoleCardsForCourse(Guid.Parse(courseID), playerCardID) 
                    }}
            };

            _sessionStorage.Save(_sessionKey, model);

            return model;
        }

        public async Task<List<PlayerCardViewModel>> AddPlayerToSessionAndReturnUpdatedPlayers(string newName)
        {
            ScoreCardViewModel sessionModel = _sessionStorage.Get(_sessionKey);

            Guid playerCardID = Guid.NewGuid();

            sessionModel.PlayerCards = (sessionModel
                .PlayerCards as IEnumerable<PlayerCardViewModel>)
                .Where(player => player.UserName != newName).Append(new PlayerCardViewModel
                {
                    UserName = newName,
                    ScoreCardID = sessionModel.ScoreCardID,
                    PlayerCardID = playerCardID,
                    HoleCards = await CreateHoleCardsForCourse(sessionModel.CourseID, playerCardID)                   
                }).ToList();

            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        private async Task<List<HoleCardViewModel>> CreateHoleCardsForCourse(Guid courseID, Guid playerCardID)
        {
            List<HoleCardViewModel> holeCardViewModelList = new List<HoleCardViewModel>();

            List<Hole> holes = await _holeRepository.GetHolesByCourseID(courseID);

            for (int i = 0; i < holes.Count; i++)
            {
                holeCardViewModelList.Add(new HoleCardViewModel
                {
                    HoleCardID = Guid.NewGuid(),
                    HoleNumber = i + 1,
                    PlayerCardID = playerCardID,
                    Score = 0
                });
            }

            return holeCardViewModelList;
        }

        public List<PlayerCardViewModel> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer)
        {
            var sessionModel = _sessionStorage.Get(_sessionKey);

            sessionModel.PlayerCards = (sessionModel.PlayerCards.Where(player => player.UserName != removePlayer)
                as IEnumerable<PlayerCardViewModel>).ToList();

            _sessionStorage.Save(_sessionKey, sessionModel);

            return sessionModel.PlayerCards;
        }

        public async Task<ScoreCardGameOnViewModel> StartScoreCard()
        {
            await _scoreCardRepository.CreateScoreCardIncludePlayerCardAsync(_mapper.Map<Models.Models.DataModels.ScoreCard>(_sessionStorage.Get(_sessionKey)));
            await _scoreCardRepository.SaveChangesAsync();

            List<Models.Models.DataModels.ScoreCard> scoreCards = await _scoreCardRepository
                .GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));

            Models.Models.DataModels.ScoreCard scoreCard = (scoreCards as IEnumerable<Models.Models.DataModels.ScoreCard>)
                .FirstOrDefault(scoreCard => scoreCard.ScoreCardID == _sessionStorage.Get(_sessionKey).ScoreCardID);

            return new ScoreCardGameOnViewModel
            {
                Hole = (await _holeRepository.GetHolesByCourseID(scoreCard.CourseID) as IEnumerable<Hole>)
                .FirstOrDefault(hole => hole.HoleNumber == 1),

                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };
        }

        public Task<ScoreCardGameOnViewModel> SaveScoreCardTurn(HoleCardViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task EndScoreCard()
        {
            throw new NotImplementedException();
        }

        public async Task<ScoreCardGameOnViewModel> ChangeHole(string activatedNextNumber, string courseID, string scorecardID)
        {
            List<Models.Models.DataModels.ScoreCard> scoreCards = await _scoreCardRepository
                .GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));

            Models.Models.DataModels.ScoreCard scoreCard = (scoreCards as IEnumerable<Models.Models.DataModels.ScoreCard>)
                .FirstOrDefault(scoreCard => scoreCard.ScoreCardID == Guid.Parse(scorecardID));

            var x =   new ScoreCardGameOnViewModel
            {
                Hole = (await _holeRepository.GetHolesByCourseID(Guid.Parse(courseID)) 
                as IEnumerable<Hole>).FirstOrDefault(hole => hole.HoleNumber == Convert.ToInt32(activatedNextNumber)),

                PagingViewModel = new PagingViewModel
                {
                    CurrentPage = Convert.ToInt32(activatedNextNumber),
                    MaxPages = scoreCard.Course.HolesTotal
                },

                ScoreCardViewModel = _mapper.Map<ScoreCardViewModel>(scoreCard)
            };

            var y = x;

            return y;

        }
    }
}
