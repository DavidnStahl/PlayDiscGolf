using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;
using System.Collections.Generic;

namespace PlayDiscGolf.Core.Services.Home
{
    public interface IHomeService
    {
        List<SearchResultAjaxFormDto> GetCourseBySearchQuery(SearchFormHomeDto model);
        public SearchFormHomeDto ConfigureCountriesAndTypes(SearchFormHomeDto model);
        List<SearchResultAjaxFormDto> TypeIsArea(SearchFormHomeDto model);
        List<SearchResultAjaxFormDto> TypeIsCourse(SearchFormHomeDto model);
    }
}
