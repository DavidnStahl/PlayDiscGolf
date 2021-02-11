using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Core.Services.CoursePage;
using PlayDiscGolf.ViewModels.Course;
using PlayDiscGolf.ViewModels.ScoreCard;

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
        public async Task<IActionResult> Index(string courseID)
        {
            var dto = await _coursePageService.GetCoursePageInformation(new Guid(courseID));



            var model = new CoursePageViewModel
            {
                CourseID = dto.CourseID,
                TotalDistance = dto.TotalDistance,
                AverageRound = dto.AverageRound,
                BestRound = dto.BestRound,
                FullName = dto.FullName,
                Holes = _mapper.Map<List<HoleViewModel>>(dto.Holes),
                Name = dto.Name,
                NumberOfRounds = dto.NumberOfRounds,
                ScoreCards = _mapper.Map<List<ScoreCardViewModel>>(dto.ScoreCards),
                TotalHoles = dto.TotalHoles,
                TotalParValue = dto.TotalParValue,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude

            };

            return View(model);
        }
      
    }
}
