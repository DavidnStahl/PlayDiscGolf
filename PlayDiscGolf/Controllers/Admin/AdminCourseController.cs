using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Services.Admin;
using PlayDiscGolf.Models.ViewModels.PostModels;
using Microsoft.AspNetCore.Authorization;

namespace PlayDiscGolf.Controllers
{
    
    public class AdminCourseController : Controller
    {
        private readonly IAdminCourseService _adminCourseService;
        private readonly IMapper _mapper;

        public AdminCourseController(IAdminCourseService adminService, IMapper mapper)
        {
            _adminCourseService = adminService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var model = new AdminSearchViewModel();
            return View(model);
        }

        public IActionResult Search([FromQuery] SearchViewModel searchModel) 
        {
            var model = _mapper.Map<List<SearchCourseItemViewModel>>(_adminCourseService.GetCoursesBySearch(searchModel));

            return PartialView("_CourseSearchResult", model);
        }

        public PartialViewResult SelectedLocation(Guid id)
        {
            var model = _mapper.Map<CourseFormViewModel>(_adminCourseService.GetCourseByID(id));

            return PartialView("_CourseForm", model);
        }
            

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditCourse(CourseFormViewModel model)
        {
            if(!ModelState.IsValid) 
                View("Index", new AdminSearchViewModel { Course = model });

            _adminCourseService.SaveUpdatedCourse(model);

            return RedirectToAction("Index");
        }

        public IActionResult GetHoles(string holes, string courseID)
        { 
            var model = new CreateHolesViewModel
            {
                NumberOfHoles = Convert.ToInt32(holes),
                CourseID = Guid.Parse(courseID),
                Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(_adminCourseService.GetCoursesHoles(Guid.Parse(courseID)))
            };

            return PartialView("_CreateHoles", model);
        }
    }
}
