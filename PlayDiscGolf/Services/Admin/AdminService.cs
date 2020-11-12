
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Locations;
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICourseRepository _courseRepository;

        public AdminService(ILocationRepository locationRepository, ICourseRepository courseRepository)
        {
            _locationRepository = locationRepository;
            _courseRepository = courseRepository;
        }
        public async Task<List<Location>> GetLocationsByQuery(string query)
        {
            return await _locationRepository.GetLocationsByQueryAsync(query);
        }

        public async Task<List<Course>> GetLocationCourses(string id)
        {
            return await _courseRepository.GetCoursesByLocationID(Convert.ToInt32(id));
        }
    }
}
