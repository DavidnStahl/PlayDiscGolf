
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
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
        private readonly IHoleRepository _holeRepository;

        public AdminService(ILocationRepository locationRepository,
                            ICourseRepository courseRepository,
                            IHoleRepository holeRepository)
        {
            _locationRepository = locationRepository;
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
        }
        public async Task<List<Location>> GetLocationsByQuery(string query)
        {
            return await _locationRepository.GetLocationsByQueryAsync(query);
        }

        public async Task<List<Course>> GetLocationCourses(Guid id)
        {
            return await _courseRepository.GetCoursesByLocationID((id));
        }

        public async Task<List<Hole>> GetCoursesHoles(Guid id)
        {
            return await _holeRepository.GetHolesByCourseID(id);
        }

        public async Task SaveUpdatedCourse(Course course)
        {
            _courseRepository.EditCourseAsync(course);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task SaveUpdatedHoles(List<Hole> holes)
        {
            _holeRepository.UpdateHoles(holes);
            await _holeRepository.SaveChangesAsync();
        }
    }
}
