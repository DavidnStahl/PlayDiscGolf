using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Controllers
{
    public class CreateCourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SaveCourse()
        {
            return View();
        }
    }
}
