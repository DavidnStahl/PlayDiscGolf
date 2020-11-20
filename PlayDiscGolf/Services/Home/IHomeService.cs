using Microsoft.AspNetCore.Mvc.Rendering;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Home
{
    public interface IHomeService
    {
        public Task<List<Course>> GetCourseBySearchQueryAsync(SearchFormHomePageViewModel model);

        public Task<SearchFormHomePageViewModel> ConfigureCountriesAndTypesAsync(SearchFormHomePageViewModel model);
        
    }
}
