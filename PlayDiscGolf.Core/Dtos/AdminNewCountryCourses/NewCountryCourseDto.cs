using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Core.Dtos.AdminNewCountryCourses
{
    public class NewCountryCourseDto
    {
        [Required]
        public string CountryCode { get; set; }

        public List<string> AddedCountryCourses { get; set; }
    }
}
