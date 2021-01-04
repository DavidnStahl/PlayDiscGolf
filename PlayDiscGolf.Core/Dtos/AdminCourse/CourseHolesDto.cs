using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.Dtos.AdminCourse
{
    public class CourseHolesDto
    {
        public Guid CourseID { get; set; }
        public Guid HoleID { get; set; }
        public int HoleNumber { get; set; }
        public int ParValue { get; set; }
        public int Distance { get; set; }
    }
}
