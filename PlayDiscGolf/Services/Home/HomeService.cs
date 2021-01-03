using Microsoft.AspNetCore.Mvc.Rendering;
using PlayDiscGolf.Data;
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

        private readonly IEntityRepository<Course> _courseRepository;

        public HomeService(IEntityRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> GetCourseBySearchQuery(SearchFormHomePageViewModel model)
        {
            if(model.Type == EnumHelper.SearchType.Area.ToString())
                return _courseRepository.FindBy(course => course.Country == model.Country && course.Area.StartsWith(model.Query)).ToList();

            return _courseRepository.FindBy(course => course.Country == model.Country && course.FullName.StartsWith(model.Query)).ToList();    
        }

        public SearchFormHomePageViewModel ConfigureCountriesAndTypes(SearchFormHomePageViewModel model)
        {
            model.Countries = _courseRepository
                .GetAll()
                .Select(x => new SelectListItem { Value = x.Country, Text = x.Country })
                .ToList();

            model.Types = new List<SelectListItem>{
                new SelectListItem(EnumHelper.SearchType.Area.ToString(), EnumHelper.SearchType.Area.ToString()),
                new SelectListItem(EnumHelper.SearchType.Course.ToString(), EnumHelper.SearchType.Course.ToString())
            };

            return model;
        }
    }
}
