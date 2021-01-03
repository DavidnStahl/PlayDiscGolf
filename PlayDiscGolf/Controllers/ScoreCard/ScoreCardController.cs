using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayDiscGolf.Services.Score;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf.Controllers.ScoreCard
{
    public class ScoreCardController : Controller
    {

        private readonly IScoreCardService _scoreCardService;

        public ScoreCardController(IScoreCardService scoreCardService)
        {
            _scoreCardService = scoreCardService;
        }

        public IActionResult CreateScoreCard(string courseID) =>
            View(_scoreCardService.GetScoreCardCreateInformation(courseID));

        public IActionResult AddPlayer(string newName) =>
            PartialView("_PlayersInPlayerCard", _scoreCardService.AddPlayerToSessionAndReturnUpdatedPlayers(newName));

        public IActionResult RemovePlayer(string removePlayer) =>
            PartialView("_PlayersInPlayerCard", _scoreCardService.RemovePlayerFromSessionAndReturnUpdatedPlayers(removePlayer));

        public IActionResult StartScoreCard() =>
            View("ScoreCardLive", _scoreCardService.StartGame());

        public IActionResult UpdateScoreCard(string scoreCardID, string holeNumber, string addOrRemove, string userName) =>
            PartialView("_ScoreCardLive", _scoreCardService.UpdateScore(scoreCardID, holeNumber, addOrRemove, userName));

        public IActionResult ChangeHole(string scoreCardID, string holeNumber, string courseID) =>
            PartialView("_ScoreCardLive", _scoreCardService.UpdateScore(scoreCardID, holeNumber, null, null));

        public IActionResult OpenScoreCard(string scoreCardID)
        {
            var model = _scoreCardService.OpenScoreCard(scoreCardID);
            return View("ScoreCardLive",model);
        }
        
            
    }
}
