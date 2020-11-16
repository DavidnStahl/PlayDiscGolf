﻿using System;
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

            if(model.Holes.Count < model.NumberOfHoles)
            {
                for (int i = model.Holes.Count; i < model.NumberOfHoles; i++)
                {
                    model.Holes.Add(new CourseFormViewModel.CourseHolesViewModel
                    {
                        CourseID = model.CourseID,
                        HoleNumber = i + 1,
                        HoleID = Guid.NewGuid(),
                        ParValue = 1,
                        Distance = 1
                    });
                }
            }
            else if(model.Holes.Count > model.NumberOfHoles)
            {
                var newHolesList = model.Holes;
                for (int i = model.NumberOfHoles; i < model.Holes.Count; i++)
                {
                    newHolesList.RemoveAt(i);
                }
                model.Holes = newHolesList;
            }
            

            return PartialView("_CreateHoles",model);
        }
    }
}
