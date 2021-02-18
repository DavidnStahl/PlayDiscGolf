using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Core.Services.Score;
using PlayDiscGolf.Core.Services.User;
using PlayDiscGolf.ViewModels.ScoreCard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Controllers.ScoreCard
{
    public class ScoreCardController : Controller
    {

        private readonly IScoreCardService _scoreCardService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ScoreCardController(
            IScoreCardService scoreCardService,
            IMapper mapper,
            IUserService userService)
        {
            _scoreCardService = scoreCardService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IActionResult> CreateScoreCard(string courseID)
        {
            var dto = _scoreCardService.GetScoreCardCreateInformation(courseID);
            var userFriends = await _userService.GetFriendsAsync();

            var model = new ScoreCardViewModel
            {
                CourseID = dto.CourseID,
                EndDate = dto.EndDate,
                StartDate = dto.StartDate,
                UserID = dto.UserID,
                PlayerCards = _mapper.Map<List<PlayerCardViewModel>>(dto.PlayerCards),
                ScoreCardID = dto.ScoreCardID,
                UserName = dto.UserName,
                Friends = userFriends.Select(x => x.UserName).ToList(),
                CourseName = _scoreCardService.GetCourseName(dto.CourseID)                
            };

            return View(model);
        }


        public async Task<IActionResult> AddPlayer(string newName)
        {
            var model = _mapper.Map<List<PlayerCardViewModel>>(await _scoreCardService.AddPlayerToSessionAndReturnUpdatedPlayersAsync(newName));

            return PartialView("_PlayersInPlayerCard", model);
        }

        public IActionResult RemovePlayer(string removePlayer)
        {
            var model = _mapper.Map<List<PlayerCardViewModel>>(_scoreCardService.RemovePlayerFromSessionAndReturnUpdatedPlayers(removePlayer));

            return PartialView("_PlayersInPlayerCard", model);
        }

        public IActionResult StartScoreCard()
        {
            var model = _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.StartGame());

            return View("ScoreCardLive", model);
        }


        public IActionResult UpdateScoreCard(string scoreCardID, string holeNumber, string addOrRemove, string userName)
        {
            var model = _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.UpdateScore(scoreCardID, holeNumber, addOrRemove, userName));

            return PartialView("_ScoreCardLive", model);
        }


        public IActionResult ChangeHole(string scoreCardID, string holeNumber)
        {
            var model = _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.UpdateScore(scoreCardID, holeNumber, null, null));

            return PartialView("_ScoreCardLive", model);
        }


        public IActionResult OpenScoreCard(string scoreCardID, int HoleNumber)
        {
            var model = _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.OpenScoreCard(scoreCardID));
            model.Hole.HoleNumber = HoleNumber;

            return View("ScoreCardLive",model);
        }                   
    }
}
