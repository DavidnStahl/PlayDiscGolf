using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels.AdminNewCountryCourses
{
    public class NewCountryCourseViewModel
    {
        [Required]
        public string CountryCode { get; set; }

        public List<string> AddedCountryCourses { get; set; }
    }
}
