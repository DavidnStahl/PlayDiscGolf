using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Services.Admin;
using PlayDiscGolf.Models.ViewModels.PostModels;
using Microsoft.AspNetCore.Authorization;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Enums;

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

        public async Task<IActionResult> Search([FromQuery]SearchViewModel model)
        {
            var searchCourseViewModel = new List<SearchCourseItemViewModel>();

            if (!string.IsNullOrWhiteSpace(model.Query) && model.Type == EnumHelper.SearchType.Area.ToString())
            {
                searchCourseViewModel = _mapper.Map<List<SearchCourseItemViewModel>>(await _adminCourseService.GetCoursesByAreaQuery(model.Query));
            }
            else
            {
                searchCourseViewModel = _mapper.Map<List<SearchCourseItemViewModel>>(await _adminCourseService.GetCoursesByCourseNameQuery(model.Query));
            }
      
            return PartialView("_CourseSearchResult", searchCourseViewModel);
        }

        public async Task<PartialViewResult> SelectedLocation(Guid id)
        {
            var model = _mapper.Map<CourseFormViewModel>(await _adminCourseService.GetCourseByID(id));
            model.Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(await _adminCourseService.GetCoursesHoles(model.CourseID));

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

            await _adminCourseService.SaveUpdatedCourse(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetHoles(string holes, string courseID)
        {
            var model = new CreateHolesViewModel
            {
                NumberOfHoles = Convert.ToInt32(holes),
                CourseID = Guid.Parse(courseID),
                Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(await _adminCourseService.GetCoursesHoles(Guid.Parse(courseID)))
            };

            model = _adminCourseService.ManageNumberOfHolesFromForm(model);

            return PartialView("_CreateHoles",model);
        }
    }
}
