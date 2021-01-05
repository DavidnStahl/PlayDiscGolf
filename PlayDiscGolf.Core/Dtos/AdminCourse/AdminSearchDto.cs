using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PlayDiscGolf.Core.Dtos.AdminCourse
{
    public class AdminSearchDto
    {
        public string Query { get; set; }
        public CourseFormDto Course = new CourseFormDto();
        public List<SearchCourseItemDto> SearchCourseItemDto = new List<SearchCourseItemDto>();

    }
}
