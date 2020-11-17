using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.ViewModels.Home;
using AutoMapper;
using PlayDiscGolf.Services.Home;

namespace PlayDiscGolf.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IMapper _mapper;

        public HomeController(IHomeService homeService, IMapper mapper)
        {
            _homeService = homeService;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                SearchFormHomePageViewModel = new SearchFormHomePageViewModel
                {
                    Countries = await _homeService.GetAllCourseCountriesAsync(),
                    Types = _homeService.SetTypes(),
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> SearchFormAjax([Bind("Type", "Country", "Query", "Types", "Countries")]SearchFormHomePageViewModel model)
        {
            model.Countries = await _homeService.GetAllCourseCountriesAsync();
            model.Types = _homeService.SetTypes();

            if (ModelState.IsValid)
            { 
                model.SearchResultAjaxFormViewModel = _mapper.Map<List<SearchResultAjaxFormViewModel>>(await _homeService.GetCourseBySearchQuery(model));

                return PartialView("_SearchFormHomePage", model);
            }
            
            return PartialView("_SearchFormHomePage", model);
        }

        
    }
}
