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
        
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                SearchFormHomePageViewModel = _homeService.ConfigureCountriesAndTypes(new SearchFormHomePageViewModel())
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult SearchFormAjax([Bind("Type", "Country", "Query", "Types", "Countries")]SearchFormHomePageViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = _homeService.ConfigureCountriesAndTypes(model);
                model.SearchResultAjaxFormViewModel = _mapper.Map<List<SearchResultAjaxFormViewModel>>(_homeService.GetCourseBySearchQuery(model));
                return PartialView("_SearchFormHomePage", model);
            }
            
            return PartialView("_SearchFormHomePage", _homeService.ConfigureCountriesAndTypes(model));
        }

        
    }
}
