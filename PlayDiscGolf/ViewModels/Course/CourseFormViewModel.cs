using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels
{
    public class CourseFormViewModel
    {
        public Guid CourseID { get; set; }

        [Display(Name = "Is this the main course")]
        public bool Main { get; set; }

        public string ApiParentID { get; set; }

        public string CountryCode { get; set; }

        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Area")]
        [Required(ErrorMessage = "Required")]
        public string Area { get; set; }
        public string ApiID { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Course name")]
        public string Name { get; set; }
        
        public string FullName { get; set; }

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

        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }

        public int NumberOfHoles { get; set; }

        public List<CourseHolesViewModel> Holes { get; set; }

        public CreateHolesViewModel CreateHoles { get; set; }

        public class CourseHolesViewModel
        {
            public Guid CourseID { get; set; }
            public Guid HoleID { get; set; }
            [Display(Name = "Hole")]
            public int HoleNumber { get; set; }
            [Required]
            [Display(Name = "Value")]
            [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
            public int ParValue { get; set; }
            [Required]
            [Display(Name = "Distance")]
            [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
            public int Distance { get; set; }
        }
    }
}
