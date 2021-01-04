using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Core.Dtos.AdminCourse
{
    public class CourseFormDto
    {
        public Guid CourseID { get; set; }

        [Display(Name = "Is this the main course")]
        public bool Main { get; set; }

        public string ApiParentID { get; set; }

        public string CountryCode { get; set; }

        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Area")]
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

        public List<CourseHolesDto> Holes { get; set; }

        public CreateHolesDto CreateHolesViewModel { get; set; }
    }
}
