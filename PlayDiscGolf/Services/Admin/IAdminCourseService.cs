
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
        Course GetCourseByID(Guid id);
        void SaveUpdatedCourse(CourseFormViewModel course);
        List<Hole> GetCoursesHoles(Guid id);
        CreateHolesViewModel ManageNumberOfHolesFromForm(CreateHolesViewModel model);
        List<Course> GetCoursesBySearch(SearchViewModel model);
    }
}
