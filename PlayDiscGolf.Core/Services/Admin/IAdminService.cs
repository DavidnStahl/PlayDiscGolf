using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.Core.Dtos.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.Services.Admin
{
    public interface IAdminService
    {
        CourseFormDto GetCourseByID(Guid id);

        List<SearchResultAjaxFormDto> GetAllCourseBySearchQuery(SearchFormHomeDto model);
        void SaveUpdatedCourse(CourseFormDto course);
        List<HoleDto> GetCoursesHoles(Guid id);
        CreateHolesDto ManageNumberOfHolesFromForm(CreateHolesDto model);
        List<CourseDto> GetCoursesBySearch(SearchDto model);
    }
}
