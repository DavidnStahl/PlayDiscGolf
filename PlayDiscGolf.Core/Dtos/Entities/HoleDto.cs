using PlayDiscGolf.Core.Dtos.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.Dtos.Entities
{
    public class HoleDto
    {
        public Guid HoleID { get; set; }
        public int HoleNumber { get; set; }
        public int ParValue { get; set; }
        public int Distance { get; set; }
        public CourseDto Course = null;
    }
}
