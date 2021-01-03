using PlayDiscGolf.Data;
using PlayDiscGolf.Models.Models.DataModels;
using System.Collections.Generic;
using System.Linq;


namespace PlayDiscGolf.Services.Admin
{
    public class AdminNewCountryCourseService : IAdminNewCountryCourseService
    {
        private readonly IEntityRepository<Course> _courseRepository;
        public AdminNewCountryCourseService(IEntityRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public List<string> GetAddedCountryCodesInCourses()
        {
            return _courseRepository.GetAll().Select(x => x.Country).Distinct().ToList();
        }
    }
}
