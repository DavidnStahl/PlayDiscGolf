using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels
{
    public class CourseFormViewModel
    {
        public int CourseID { get; set; }

        [Display(Name = "Is this the main course")]
        public bool Main { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Course name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Total number of holes")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int HolesTotal { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Total par value")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int TotalParValue { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Total course distance")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int TotalDistance { get; set; }
        public int LocationID { get; set; }
    }
}
