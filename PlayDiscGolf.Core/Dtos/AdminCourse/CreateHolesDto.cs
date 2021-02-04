using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Core.Dtos.Home;
using System;
using System.Collections.Generic;

namespace PlayDiscGolf.Core.Dtos.AdminCourse
{
    public class CreateHolesDto
    {
        public int NumberOfHoles { get; set; }
        public Guid CourseID { get; set; }
        public List<HoleDto> Holes = new List<HoleDto>();       
    }
}
