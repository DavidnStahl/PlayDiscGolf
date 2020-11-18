using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Services.CoursePage;

namespace PlayDiscGolf.Controllers.CoursePage
{
    public class CoursePageController : Controller
    {
        private readonly ICoursePageService _coursePageService;

        public CoursePageController(ICoursePageService coursePageService)
        {
            _coursePageService = coursePageService;
        }
        public async Task<IActionResult> Index(string courseID)
        {
            var model = await _coursePageService.GetCoursePageInformationAsync(new Guid(courseID));
            return View(model);
        }
    }
}
