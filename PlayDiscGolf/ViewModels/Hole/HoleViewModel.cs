using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.Course
{
    public class HoleViewModel
    {
        public Guid HoleID { get; set; }
        public int HoleNumber { get; set; }
        public int ParValue { get; set; }
        public int Distance { get; set; }
        public CourseViewModel Course = null;
    }
}
