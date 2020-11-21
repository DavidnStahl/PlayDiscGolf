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

        public async Task<IActionResult> CreateScoreCard(string courseID) =>
            View(await _scoreCardService.GetScoreCardCreateInformationAsync(courseID));

        public async Task<IActionResult> AddPlayer(string newName) =>
            PartialView("_PlayersInPlayerCard", await _scoreCardService.AddPlayerToSessionAndReturnUpdatedPlayersAsync(newName));

        public IActionResult RemovePlayer(string removePlayer) =>
            PartialView("_PlayersInPlayerCard", _scoreCardService.RemovePlayerFromSessionAndReturnUpdatedPlayers(removePlayer));

        public async Task<IActionResult> StartScoreCard() =>
            View("ScoreCardLive",await _scoreCardService.StartScoreCardAsync());

        public async Task<IActionResult> UpdateScoreCard(string scoreCardID, string holeNumber, string addOrRemove, string userName) =>
            PartialView("_ScoreCardLive", await _scoreCardService.ModifyScoreCardAsync(scoreCardID, holeNumber, addOrRemove, userName));

        public async Task<IActionResult> ChangeHole(string scoreCardID, string holeNumber) =>
            PartialView("_ScoreCardLive", await _scoreCardService.ModifyScoreCardAsync(scoreCardID, holeNumber, null, null));
    }
}
