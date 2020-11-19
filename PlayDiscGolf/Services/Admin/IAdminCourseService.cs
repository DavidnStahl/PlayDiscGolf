
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public interface IAdminCourseService
    {
        public Task<Course> GetCourseByID(Guid id);
        public Task SaveUpdatedCourse(CourseFormViewModel course);
        public Task<List<Hole>> GetCoursesHoles(Guid id);
        public CreateHolesViewModel ManageNumberOfHolesFromForm(CreateHolesViewModel model);

        public Task<List<Course>> GetCoursesBySearch(SearchViewModel model);
    }
}
