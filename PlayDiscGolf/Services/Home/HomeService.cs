using Microsoft.AspNetCore.Mvc.Rendering;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Home
{
    public class HomeService : IHomeService
    {

        private readonly ICourseRepository _courseRepository;

        public HomeService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        
        public async Task<List<SelectListItem>> GetAllCourseCountriesAsync() =>
            (await _courseRepository.GetAllCoursesCountriesAsync() as IEnumerable<string>).Select(country => 
            new SelectListItem { Value = country, Text = country}).ToList();

        public List<SelectListItem> SetTypes() =>
            new List<SelectListItem>{
                new SelectListItem(EnumHelper.SearchType.Area.ToString(), EnumHelper.SearchType.Area.ToString()),
                new SelectListItem(EnumHelper.SearchType.Course.ToString(), EnumHelper.SearchType.Course.ToString())
            };

        public async Task<List<Course>> GetCourseBySearchQuery(SearchFormHomePageViewModel model) =>
            (model.Type == EnumHelper.SearchType.Area.ToString()) ?
            await _courseRepository.GetCoursesByCountryAreaAndQueryAsync(model.Country, model.Query) :
            await _courseRepository.GetCoursesByCountryFullNameAndQueryAsync(model.Country, model.Query);

        public async Task<SearchFormHomePageViewModel> ConfigureCountriesAndTypes(SearchFormHomePageViewModel model)
        {
            model.Countries = await GetAllCourseCountriesAsync();

            model.Types = SetTypes();

            return model;
        }
    }
}
