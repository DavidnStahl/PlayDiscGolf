using System;
using System.Collections.Generic;

namespace PlayDiscGolf.Core.Dtos.AdminCourse
{
    public class CreateHolesDto
    {
        public int NumberOfHoles { get; set; }
        public Guid CourseID { get; set; }

        public List<CourseHolesDto> Holes = new List<CourseHolesDto>();       
    }
}
