using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf.Controllers.ScoreCard
{
    public class ScoreCardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ScoreCardController(UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult CreateScoreCard(string courseID)
        {
            var model = new ScoreCardViewModel
            {
                CourseID = courseID,
                UserName = User.Identity.Name,
                UserID = _userManager.GetUserId(User),
                ScoreCardID = Guid.NewGuid().ToString()
            };

            model.PlayerCards.Add(new PlayerCardViewModel
            {
                UserID = _userManager.GetUserId(User),
                UserName = User.Identity.Name,
                PlayerCardID = Guid.NewGuid().ToString(),
                ScoreCardID = model.ScoreCardID
            });

            string json = JsonConvert.SerializeObject(model);
            _httpContextAccessor.HttpContext.Session.SetString("ScoreCardViewModel",json);

            return View(model);
        }

        public IActionResult AddPlayer(string newName)
        {
            var str = _httpContextAccessor.HttpContext.Session.GetString("ScoreCardViewModel");
            var sessionModel = JsonConvert.DeserializeObject<ScoreCardViewModel>(str);

            sessionModel.PlayerCards = (sessionModel.PlayerCards.Where(player => player.UserName != newName)
                as IEnumerable<PlayerCardViewModel>).ToList();

            sessionModel.PlayerCards.Add(new PlayerCardViewModel
            {
                UserName = newName,
                ScoreCardID = sessionModel.ScoreCardID,
                PlayerCardID = Guid.NewGuid().ToString()
            });

            string json = JsonConvert.SerializeObject(sessionModel);
            _httpContextAccessor.HttpContext.Session.SetString("ScoreCardViewModel", json);

            return PartialView("_PlayersInPlayerCard", sessionModel.PlayerCards);
        }

        public IActionResult RemovePlayer(string removePlayer)
        {
            var str = _httpContextAccessor.HttpContext.Session.GetString("ScoreCardViewModel");
            var sessionModel = JsonConvert.DeserializeObject<ScoreCardViewModel>(str);

            sessionModel.PlayerCards = (sessionModel.PlayerCards.Where(player => player.UserName != removePlayer)
                as IEnumerable<PlayerCardViewModel>).ToList();

            string json = JsonConvert.SerializeObject(sessionModel);
            _httpContextAccessor.HttpContext.Session.SetString("ScoreCardViewModel", json);

            return PartialView("_PlayersInPlayerCard", sessionModel.PlayerCards);
        }
        public IActionResult StartScoreCard(string courseID)
        {
            return View();
        }

        public IActionResult EditScoreCard(string scourseCardid)
        {
            return View();
        }

        public IActionResult DeleteScoreCard(string scourseCardid)
        {
            return View();
        }

        public IActionResult ClaimScoreCard(string userID)
        {
            return View();
        }
    }
}
