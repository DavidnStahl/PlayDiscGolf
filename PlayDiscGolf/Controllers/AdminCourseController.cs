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

        public async Task<PartialViewResult> SelectedLocation(string id)
        {
            var model = _mapper.Map<List<CourseFormViewModel>>(await _adminService.GetLocationCourses(id));

            return PartialView("_CourseForm", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_CourseFormPartial", model);
            }

            await _adminService.SaveUpdatedCourse(_mapper.Map<Course>(model));

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CreateCourseViewModel model)
        {

        }

        [HttpGet]
        public IActionResult AddCourse(string id)
        {
            var model = new CreateCourseViewModel
            {
                LocationID = Convert.ToInt32(id)
            };

            return View(model);
        }
    }
}
