
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
        public Task<Course> GetCourseByIDAsync(Guid id);
        public Task SaveUpdatedCourseAsync(CourseFormViewModel course);
        public Task<List<Hole>> GetCoursesHolesAsync(Guid id);
        public CreateHolesViewModel ManageNumberOfHolesFromForm(CreateHolesViewModel model);

        public Task<List<Course>> GetCoursesBySearchAsync(SearchViewModel model);
    }
}
