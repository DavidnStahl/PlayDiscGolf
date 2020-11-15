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


        public async Task<IActionResult> Search(string query, string searchType)
        {
            var model = new List<SearchCourseItemViewModel>();

            if (!string.IsNullOrWhiteSpace(query) && searchType == "1")
            {
                model = _mapper.Map<List<SearchCourseItemViewModel>>(await _adminService.GetCoursesByLocationQuery(query));
            }
            else if(!string.IsNullOrWhiteSpace(query) && searchType == "2")
            {
                model = _mapper.Map<List<SearchCourseItemViewModel>>(await _adminService.GetCoursesByCourseNameQuery(query));
            }
               

            return PartialView("_CourseSearchResult", model);
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
                var parentModel = new AdminSearchViewModel();
                parentModel.Course = model;
                return View("Index", parentModel);
            }

            await _adminService.SaveUpdatedCourse(_mapper.Map<Course>(model));
            if(model.Holes != null)
            {
                await _adminService.SaveUpdatedHoles(_mapper.Map<List<Hole>>(model.Holes));
            }

           /* if(model.Holes != null)
            {
                await _adminService.AddHolesToCourse(_mapper.Map<List<Hole>>(model.Holes));
            }*/

            return RedirectToAction("Index");
        }

        public IActionResult CreateHoles(string holes, string courseID)
        {
            var model = new CreateHolesViewModel
            {
                NumberOfHoles = Convert.ToInt32(holes),
                CourseID = Guid.Parse(courseID)               
            };


            for (int i = 0; i < model.NumberOfHoles; i++)
            {
                model.Holes.Add(new CourseFormViewModel.CourseHolesViewModel
                {
                    CourseID = model.CourseID,
                    HoleNumber = i + 1,
                    HoleID = Guid.NewGuid()
                });
            }

            return PartialView("_CreateHoles",model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateHoles(CreateHolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _adminService.AddHolesToCourse(_mapper.Map<List<Hole>>(model.Holes));

            return RedirectToAction("Index");
        }
    }
}
