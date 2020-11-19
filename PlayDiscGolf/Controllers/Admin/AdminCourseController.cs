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
        public IActionResult Index() =>
            View(new AdminSearchViewModel());

        public async Task<IActionResult> Search([FromQuery] SearchViewModel model) =>
            PartialView("_CourseSearchResult", _mapper.Map<List<SearchCourseItemViewModel>>(await _adminCourseService.GetCoursesBySearch(model)));

        public async Task<PartialViewResult> SelectedLocation(Guid id) =>
            PartialView("_CourseForm", _mapper.Map<CourseFormViewModel>(await _adminCourseService.GetCourseByID(id)));

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseFormViewModel model)
        {
            if(!ModelState.IsValid) View("Index", new AdminSearchViewModel { Course = model });

            await _adminCourseService.SaveUpdatedCourse(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetHoles(string holes, string courseID) =>
            PartialView("_CreateHoles", _adminCourseService.ManageNumberOfHolesFromForm(new CreateHolesViewModel {
                NumberOfHoles = Convert.ToInt32(holes),
                CourseID = Guid.Parse(courseID),
                Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(await _adminCourseService.GetCoursesHoles(Guid.Parse(courseID)))
            }));
    }
}
