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
        
        public async Task<List<SelectListItem>> GetAllCourseCountriesAsync()
        {
            var countries = await _courseRepository.GetAllCoursesCountriesAsync();

            var selectListItemList = new List<SelectListItem>();

            for (int i = 0; i < countries.Count; i++)
            {
                selectListItemList.Add(new SelectListItem(countries[i], countries[i]));
            }

            return selectListItemList;
        }

        public List<SelectListItem> SetTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem(EnumHelper.SearchType.Area.ToString(), EnumHelper.SearchType.Area.ToString()),
                new SelectListItem(EnumHelper.SearchType.Course.ToString(), EnumHelper.SearchType.Course.ToString())
            };
        }

        public async Task<List<Course>> GetCourseBySearchQuery(SearchFormHomePageViewModel model)
        {
            if (model.Type == EnumHelper.SearchType.Area.ToString())
                return await _courseRepository.GetCoursesByCountryAreaAndQueryAsync(model.Country, model.Query);

            return await _courseRepository.GetCoursesByCountryFullNameAndQueryAsync(model.Country, model.Query);
        }

        
    }
}
