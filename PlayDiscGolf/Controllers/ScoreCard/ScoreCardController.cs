using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlayDiscGolf.Controllers.ScoreCard
{
    public class ScoreCardController : Controller
    {
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
