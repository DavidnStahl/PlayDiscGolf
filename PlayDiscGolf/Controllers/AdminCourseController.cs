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
            var model = new AdminSearchViewModel
            {
                SearchLocationItemViewModels = new List<SearchLocationItemViewModel>()
            };

            return View(model);
        }


        public async Task<IActionResult> Search(string query)
        {
            var model = new List<SearchLocationItemViewModel>();

            if (!string.IsNullOrWhiteSpace(query))
               model = _mapper.Map<List<SearchLocationItemViewModel>>(await _adminService.GetLocationsByQuery(query));

            return PartialView("_LocationSearchResult", model);
        }

        public async Task<IActionResult> SelectedLocation(string id)
        {
            var x = await _adminService.GetLocationCourses(id);
            var y = 1;

            return View();
        }
    }
}
