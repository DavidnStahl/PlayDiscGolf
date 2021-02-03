using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.ViewModels.Home;
using AutoMapper;
using PlayDiscGolf.Core.Services.Home;
using PlayDiscGolf.Core.Dtos.Home;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

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
            var model = SetCountriesAndTypesViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult SearchFormAjax([Bind("Type", "Country", "Query", "Types", "Countries")]SearchFormHomePageViewModel model)
        {
            var searchOptionModel = SetCountriesAndTypesViewModel();
            model.Types = searchOptionModel.SearchFormHomePageViewModel.Types;
            model.Countries = searchOptionModel.SearchFormHomePageViewModel.Countries;

            if (ModelState.IsValid)
            {
                model = GetCourseBySearchQuery(model);

                return PartialView("_SearchFormHomePage", model);
            }
            
            return PartialView("_SearchFormHomePage", _homeService.ConfigureCountriesAndTypes(_mapper.Map<SearchFormHomeDto>(model)));
        }

        private SearchFormHomePageViewModel GetCourseBySearchQuery(SearchFormHomePageViewModel model)
        {

            var dto = _homeService.GetCourseBySearchQuery(new SearchFormHomeDto
            {
                Countries = model.Countries.Select(x => x.Text).ToList(),
                Types = model.Types.Select(x => x.Text).ToList(),
                Country = model.Country,
                Query = model.Query,
                Type = model.Type
            });

            model.SearchResultAjaxFormViewModel = _mapper.Map<List<SearchResultAjaxFormViewModel>>(dto);

            return model;
        }

        private HomeViewModel SetCountriesAndTypesViewModel()
        {
            var model = new HomeViewModel();
            var dto = _homeService.ConfigureCountriesAndTypes(new SearchFormHomeDto());

            foreach (var country in dto.Countries)
            {
                model.SearchFormHomePageViewModel.Countries.Add(new SelectListItem { Value = country, Text = country });
            }

            foreach (var type in dto.Types)
            {
                model.SearchFormHomePageViewModel.Types.Add(new SelectListItem { Value = type, Text = type });
            }

            return model;
        }
    }
}
