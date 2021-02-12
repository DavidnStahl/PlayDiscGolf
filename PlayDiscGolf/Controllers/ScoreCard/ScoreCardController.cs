using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Core.Services.Score;
using PlayDiscGolf.ViewModels.ScoreCard;
using System.Collections.Generic;

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

        public IActionResult CreateScoreCard(string courseID)
        {
            var dto = _scoreCardService.GetScoreCardCreateInformation(courseID);

            var model = new ScoreCardViewModel
            {
                CourseID = dto.CourseID,
                EndDate = dto.EndDate,
                StartDate = dto.StartDate,
                UserID = dto.UserID,
                PlayerCards = _mapper.Map<List<PlayerCardViewModel>>(dto.PlayerCards),
                ScoreCardID = dto.ScoreCardID,
                UserName = dto.UserName
            };

            return View(model);
        }


        public IActionResult AddPlayer(string newName)
        {
            var model = _mapper.Map<List<PlayerCardViewModel>>(_scoreCardService.AddPlayerToSessionAndReturnUpdatedPlayers(newName));

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


        public IActionResult OpenScoreCard(string scoreCardID, int HoleNumber = 1)
        {
            var model = _mapper.Map<ScoreCardGameOnViewModel>(_scoreCardService.OpenScoreCard(scoreCardID));
            model.Hole.HoleNumber = HoleNumber;

            return View("ScoreCardLive",model);
        }
        
            
    }
}
