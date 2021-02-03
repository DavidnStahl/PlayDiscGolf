using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
using Microsoft.AspNetCore.Authorization;
using PlayDiscGolf.Core.Services.Admin;
using PlayDiscGolf.Core.Dtos.PostModels;
using PlayDiscGolf.Core.Dtos.AdminCourse;

namespace PlayDiscGolf.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
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
            var dto = _adminService.GetCoursesBySearch(_mapper.Map<SearchDto>(searchModel));
            var model = _mapper.Map<List<SearchCourseItemViewModel>>(dto);

            return PartialView("_CourseSearchResult", model);
        }

        public PartialViewResult SelectedLocation(Guid id)
        {
            var dto = _adminService.GetCourseByID(id);
            var model = _mapper.Map<CourseFormViewModel>(dto);

            return PartialView("_CourseForm", model);
        }
            

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditCourse(CourseFormViewModel model)
        {
            if(!ModelState.IsValid) 
                View("Index", new AdminSearchViewModel { Course = model });

            var dto = _mapper.Map<CourseFormDto>(model);
            _adminService.SaveUpdatedCourse(dto);

            return RedirectToAction("Index");
        }

        public IActionResult GetHoles(string holes, string courseID)
        { 
            var model = new CreateHolesViewModel
            {
                NumberOfHoles = Convert.ToInt32(holes),
                CourseID = Guid.Parse(courseID),
                Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(_adminService.GetCoursesHoles(Guid.Parse(courseID)))
            };

            //Mappning behöver fixas
            var x = _mapper.Map<CreateHolesViewModel>(_adminService.ManageNumberOfHolesFromForm(_mapper.Map<CreateHolesDto>(model)));

            
            return PartialView("_CreateHoles", x);
        }
    }
}
