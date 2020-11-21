﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Models.ViewModels.AdminNewCountryCourses;
using PlayDiscGolf.Services.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminNewCountryCoursesController : Controller
    {
        private readonly IAdminNewCountryCourseService _adminNewCountryCourseService;
        private readonly IMapper _mapper;

        public AdminNewCountryCoursesController(IAdminNewCountryCourseService adminNewCountryCourseService, IMapper mapper)
        {
            _adminNewCountryCourseService = adminNewCountryCourseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index() =>
            View(new NewCountryCourseViewModel {
                AddedCountryCourses = await _adminNewCountryCourseService.GetAddedCountryCodesInCoursesAsync() 
            });
    }
}
