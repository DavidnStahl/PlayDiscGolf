using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Services.Admin;
using Newtonsoft.Json;
using PlayDiscGolf.Models.DataModels;

namespace PlayDiscGolf.Controllers
{
    public class AdminCourseController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminCourseController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new AdminSearchViewModel();

            return View(model);
        }


        public async Task<IActionResult> Search(string query)
        {
            var model = new List<SearchLocationItemViewModel>();

            if (!string.IsNullOrWhiteSpace(query))
               model = _mapper.Map<List<SearchLocationItemViewModel>>(await _adminService.GetLocationsByQuery(query));

            return PartialView("_LocationSearchResult", model);
        }

        public async Task<PartialViewResult> SelectedLocation(Guid id)
        {
            var model = _mapper.Map<List<CourseFormViewModel>>(await _adminService.GetLocationCourses(id));

            foreach (var course in model)
            {

                course.Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(await _adminService.GetCoursesHoles(course.CourseID));
            }

            return PartialView("_CourseForm", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var parentModel = new AdminSearchViewModel();
                parentModel.Courses.Add(model);
                return View("Index", parentModel);
            }

            await _adminService.SaveUpdatedCourse(_mapper.Map<Course>(model));
            await _adminService.SaveUpdatedHoles(_mapper.Map<List<Hole>>(model.Holes));

            return RedirectToAction("Index");
        }

        /*[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CreateCourseViewModel model)
        {

        }*/

        [HttpGet]
        public IActionResult AddCourse(Guid id)
        {
            var model = new CreateCourseViewModel
            {
                LocationID = id
            };

            return View(model);
        }
    }
}
