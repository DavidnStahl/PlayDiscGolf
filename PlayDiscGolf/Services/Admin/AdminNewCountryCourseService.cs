using PlayDiscGolf.Data.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminNewCountryCourseService : IAdminNewCountryCourseService
    {
        private readonly ICourseRepository _courseRepository;
        public AdminNewCountryCourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<List<string>> GetAddedCountryCodesInCoursesAsync() => await _courseRepository.GetAllCoursesCountriesAsync();
    }
}
