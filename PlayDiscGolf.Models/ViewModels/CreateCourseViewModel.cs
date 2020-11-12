using PlayDiscGolf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels
{
    public class CreateCourseViewModel
    {
        public string CourseName { get; set; }

        public string CountryCode { get; set; }

        public string City { get; set; }
        public string County { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

      
    }
}
