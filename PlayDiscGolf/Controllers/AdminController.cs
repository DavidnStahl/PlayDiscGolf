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
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
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

        public async Task<JsonResult> AjaxCallGetLocationsByQuery(string query)
        {
            var model = new List<SearchLocationItemViewModel>();

            if (query.Length >= 3)
                model = _mapper.Map<List<SearchLocationItemViewModel>>(await _adminService.GetLocationsByQuery(query));

            var json = JsonConvert.SerializeObject(model);

            return ;
        }
    }
}
