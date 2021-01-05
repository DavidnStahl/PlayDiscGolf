using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Core.Services.CoursePage;
using PlayDiscGolf.ViewModels.Course;

namespace PlayDiscGolf.Controllers.CoursePage
{
    public class CourseController : Controller
    {
        private readonly ICoursePageService _coursePageService;
        private readonly IMapper _mapper;

        public CourseController(ICoursePageService coursePageService, IMapper mapper)
        {
            _coursePageService = coursePageService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string courseID) =>
            View(_mapper.Map<CoursePageViewModel>(await _coursePageService.GetCoursePageInformationAsync(new Guid(courseID))));
      
    }
}
