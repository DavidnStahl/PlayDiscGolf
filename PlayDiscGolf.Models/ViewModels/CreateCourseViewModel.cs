using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels
{
    public class CreateCourseViewModel
    {
        public Guid LocationID { get; set; }

        [Required]
        [Display(Name = "Is this the main course")]
        public bool Main { get; set; }

        [Required]
        [Display(Name = "Course name")]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Total number of holes")]
        public int HolesTotal { get; set; }

        [Required]
        [Display(Name = "Total par value")]
        public int TotalParValue { get; set; }

        [Required]
        [Display(Name = "Total course distance")]
        public int TotalDistance { get; set; }
    }
}
