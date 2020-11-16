
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public interface IAdminService
    {
        public Task<List<Course>> GetCoursesByLocationQuery(string query);

        public Task<List<Course>> GetCoursesByCourseNameQuery(string query);
        public Task<Course> GetCourseByID(Guid id);
        public Task SaveUpdatedCourse(Course course);
        public Task<List<Hole>> GetCoursesHoles(Guid id);

        public Task SaveNewHoles(List<Hole> holes);

        public Task DeleteHoles(List<Hole> holes);

        public Task CheckAndRemoveOrAddHole(Course course);
    }
}
