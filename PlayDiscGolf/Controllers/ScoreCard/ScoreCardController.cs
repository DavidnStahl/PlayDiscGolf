using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayDiscGolf.Services.ScoreCard;
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

        public async Task<IActionResult> StartScoreCard() =>
            View("ScoreCardLive",await _scoreCardService.StartScoreCard());

        public IActionResult SaveScoreCardTurn(HoleCardViewModel model) =>
            View(_scoreCardService.SaveScoreCardTurn(model));

        public IActionResult ChangeHole(string activatedNextNumber, string courseID, string scorecardID) =>
            View(_scoreCardService.ChangeHole(activatedNextNumber, courseID, scorecardID));

        public IActionResult EndScoreCard() =>
            View(_scoreCardService.EndScoreCard());
    }
}
