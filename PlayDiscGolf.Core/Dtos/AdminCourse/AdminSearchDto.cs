using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PlayDiscGolf.Core.Dtos.AdminCourse
{
    public class AdminSearchDto
    {
        [Display(Name = "Search location")]
        public string Query { get; set; }

        public CourseFormDto Course = new CourseFormDto();


        public List<SearchCourseItemDto> SearchCourseItemViewModels = new List<SearchCourseItemDto>();

    }
}
