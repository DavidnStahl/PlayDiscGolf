using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;
using System.Collections.Generic;

namespace PlayDiscGolf.Core.Services.Home
{
    public interface IHomeService
    {
        public List<CourseDto> GetCourseBySearchQuery(SearchFormHomeDto model);

        public SearchFormHomeDto ConfigureCountriesAndTypes(SearchFormHomeDto model);
        
    }
}
