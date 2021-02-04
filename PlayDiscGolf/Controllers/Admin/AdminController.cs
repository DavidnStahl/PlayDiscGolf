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
using PlayDiscGolf.Core.Dtos.Entities;

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
            model.Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(_adminService.GetCoursesHoles(model.CourseID));
            model.CreateHoles.CourseID = dto.CourseID;
            model.CreateHoles.NumberOfHoles = dto.NumberOfHoles;
            model.CreateHoles.Holes = model.Holes;
            model.Name = dto.Name;

            return PartialView("_CourseForm", model);
        }
            

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditCourse(CourseFormViewModel model)
        {
            if(!ModelState.IsValid) 
                View("Index", new AdminSearchViewModel { Course = model });

            MapAndSaveCourse(model);

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
            var dto = _adminService.ManageNumberOfHolesFromForm(_mapper.Map<CreateHolesDto>(model));

            model.NumberOfHoles = dto.NumberOfHoles;
            model.CourseID = dto.CourseID;
            model.Holes = _mapper.Map<List<CourseFormViewModel.CourseHolesViewModel>>(dto.Holes);


            return PartialView("_CreateHoles", model);
        }

        private void MapAndSaveCourse(CourseFormViewModel model)
        {
            var dto = new CourseFormDto
            {
                CountryCode = model.CountryCode,
                HolesTotal = model.HolesTotal,
                NumberOfHoles = model.NumberOfHoles,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                ApiParentID = model.ApiParentID,
                ApiID = model.ApiID,
                CourseID = model.CourseID,
                Area = model.Area,
                TotalDistance = model.TotalDistance,
                TotalParValue = model.TotalParValue,
                Main = model.Main,
                FullName = model.FullName,
                Name = model.Name,
                Holes = _mapper.Map<List<CourseHolesDto>>(model.Holes),
            };

            dto.CreateHoles.CourseID = model.CreateHoles.CourseID;
            dto.CreateHoles.NumberOfHoles = model.CreateHoles.NumberOfHoles;
            dto.CreateHoles.Holes = _mapper.Map<List<HoleDto>>(model.CreateHoles.Holes);

            _adminService.SaveUpdatedCourse(dto);
        }
    }
}
