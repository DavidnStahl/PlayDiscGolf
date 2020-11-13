
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public interface IAdminService
    {
        public Task<List<Location>> GetLocationsByQuery(string query);
        public Task<List<Course>> GetLocationCourses(Guid id);
        public Task SaveUpdatedCourse(Course course);
        public Task<List<Hole>> GetCoursesHoles(Guid id);

        public Task SaveUpdatedHoles(List<Hole> holes);
    }
}
