using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.Dtos.AdminCourse
{
    public class SearchCourseItemDto
    {       
        public Guid CourseID { get; set; }
        public string FullName { get; set; }  
    }
}
