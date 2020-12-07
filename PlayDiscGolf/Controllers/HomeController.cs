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
        
        public async Task<IActionResult> Index() =>
            View(new HomeViewModel 
            { 
                SearchFormHomePageViewModel = await _homeService.ConfigureCountriesAndTypesAsync(new SearchFormHomePageViewModel())
            });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> SearchFormAjax([Bind("Type", "Country", "Query", "Types", "Countries")]SearchFormHomePageViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = await _homeService.ConfigureCountriesAndTypesAsync(model);

                model.SearchResultAjaxFormViewModel = _mapper.Map<List<SearchResultAjaxFormViewModel>>(await _homeService.GetCourseBySearchQueryAsync(model));

                return PartialView("_SearchFormHomePage", model);
            }
            
            return PartialView("_SearchFormHomePage", await _homeService.ConfigureCountriesAndTypesAsync(model));
        }

        
    }
}
