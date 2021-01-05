using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Core.Services.Score;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf.Controllers.ScoreCard
{
    public class ScoreCardController : Controller
    {

        private readonly IScoreCardService _scoreCardService;
        private readonly IMapper _mapper;

        public ScoreCardController(
            IScoreCardService scoreCardService,
            IMapper mapper)
        {
            _scoreCardService = scoreCardService;
            _mapper = mapper;
        }

        public IActionResult CreateScoreCard(string courseID) =>
            View(_scoreCardService.GetScoreCardCreateInformation(courseID));

        public IActionResult AddPlayer(string newName) =>
            PartialView("_PlayersInPlayerCard",_mapper.Map<PlayerCardViewModel>(_scoreCardService.AddPlayerToSessionAndReturnUpdatedPlayers(newName)));

        public IActionResult RemovePlayer(string removePlayer) =>
            PartialView("_PlayersInPlayerCard", _mapper.Map<PlayerCardViewModel>( _scoreCardService.RemovePlayerFromSessionAndReturnUpdatedPlayers(removePlayer)));

        public IActionResult StartScoreCard() =>
            View("ScoreCardLive", _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.StartGame()));

        public IActionResult UpdateScoreCard(string scoreCardID, string holeNumber, string addOrRemove, string userName) =>
            PartialView("_ScoreCardLive", _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.UpdateScore(scoreCardID, holeNumber, addOrRemove, userName)));

        public IActionResult ChangeHole(string scoreCardID, string holeNumber) =>
            PartialView("_ScoreCardLive", _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.UpdateScore(scoreCardID, holeNumber, null, null)));

        public IActionResult OpenScoreCard(string scoreCardID)
        {
            var model = _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.OpenScoreCard(scoreCardID));
            return View("ScoreCardLive",model);
        }
        
            
    }
}
