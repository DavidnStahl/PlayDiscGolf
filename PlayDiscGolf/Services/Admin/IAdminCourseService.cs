
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public interface IAdminCourseService
    {
        public Task<List<Course>> GetCoursesByAreaQuery(string query);

        public Task<List<Course>> GetCoursesByCourseNameQuery(string query);
        public Task<Course> GetCourseByID(Guid id);
        public Task SaveUpdatedCourse(CourseFormViewModel course);
        public Task<List<Hole>> GetCoursesHoles(Guid id);

        public CreateHolesViewModel ManageNumberOfHolesFromForm(CreateHolesViewModel model);
    }
}
