
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminService : IAdminService
    {       
        private readonly ICourseRepository _courseRepository;
        private readonly IHoleRepository _holeRepository;

        public AdminService(ICourseRepository courseRepository,
                            IHoleRepository holeRepository)
        {
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
        }

        public async Task<List<Hole>> GetCoursesHoles(Guid id)
        {
            return await _holeRepository.GetHolesByCourseID(id);
        }

        public async Task SaveUpdatedCourse(Course course)
        {
            course.Holes = null;
            _courseRepository.EditCourseAsync(course);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task SaveUpdatedHoles(List<Hole> holes)
        {
            _holeRepository.UpdateHoles(holes);
            await _holeRepository.SaveChangesAsync();
        }

        public async Task<Course> GetCourseByID(Guid id)
        {
            return await _courseRepository.GetCourseByIDAsync((id));
        }

        public async Task<List<Course>> GetCoursesByLocationQuery(string query)
        {
            return await _courseRepository.GetCoursesByAreaQueryAsync(query);
        }

        public async Task<List<Course>> GetCoursesByCourseNameQuery(string query)
        {
            return await _courseRepository.GetCoursesByFullNameQueryAsync(query);
        }

        public async Task AddHolesToCourse(List<Hole> holes)
        {
            await _holeRepository.CreateHolesAsync(holes);
        }
    }
}
