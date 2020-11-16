using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Services.Admin;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
using System.Linq;

namespace PlayDiscGolf.Controllers
{
    public class AdminCourseController : Controller
    {
        private readonly IAdminCourseService _adminService;
        private readonly IMapper _mapper;

        public AdminCourseController(IAdminCourseService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new AdminSearchViewModel();

            return View(model);
        }

        public async Task<IActionResult> Search([FromQuery]SearchViewModel model)
        {
            /*if(!ModelState.IsValid)
            {
                //HttpContext.Response.StatusCode = 400;
                PartialView("_ErrorSearch", model);
                //return Json(new { success = false, issue = model, errors = ModelState.Values.Where(i => i.Errors.Count > 0) });
            }*/
            var searchCourseViewModel = new List<SearchCourseItemViewModel>();

            if (!string.IsNullOrWhiteSpace(model.Query) && model.Type == "Location")
            {
                searchCourseViewModel = _mapper.Map<List<SearchCourseItemViewModel>>(await _adminService.GetCoursesByAreaQuery(model.Query));
            }
            else
            {
                searchCourseViewModel = _mapper.Map<List<SearchCourseItemViewModel>>(await _adminService.GetCoursesByCourseNameQuery(model.Query));
            }
      
            return PartialView("_CourseSearchResult", searchCourseViewModel);
        }

        public async Task<PartialViewResult> SelectedLocation(Guid id)
        {
            var model = _mapper.Map<CourseFormViewModel>(await _adminService.GetCourseByID(id));
            model.Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(await _adminService.GetCoursesHoles(model.CourseID));

            return PartialView("_CourseForm", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var parentModel = new AdminSearchViewModel
                {
                    Course = model
                };

                return View("Index", parentModel);
            }

            await _adminService.SaveUpdatedCourse(_mapper.Map<Course>(model));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetHoles(string holes, string courseID)
        {
            var model = new CreateHolesViewModel
            {
                NumberOfHoles = Convert.ToInt32(holes),
                CourseID = Guid.Parse(courseID),
                Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(await _adminService.GetCoursesHoles(Guid.Parse(courseID)))
            };

            model = _adminService.ManageNumberOfHolesFromForm(model);

            return PartialView("_CreateHoles",model);
        }
    }
}
