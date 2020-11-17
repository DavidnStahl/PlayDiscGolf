using Microsoft.AspNetCore.Mvc.Rendering;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Home
{
    public interface IHomeService
    {
        public Task<List<SelectListItem>> GetAllCourseCountriesAsync();

        public List<SelectListItem> SetTypes();

        public Task<List<Course>> GetCourseBySearchQuery(SearchFormHomePageViewModel model);
    }
}
