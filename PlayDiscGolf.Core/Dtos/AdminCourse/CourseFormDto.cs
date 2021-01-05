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
        public bool Main { get; set; }
        public string ApiParentID { get; set; }
        public string CountryCode { get; set; }
        public string Area { get; set; }
        public string ApiID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int HolesTotal { get; set; }
        public int TotalParValue { get; set; }
        public int TotalDistance { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int NumberOfHoles { get; set; }
        public List<CourseHolesDto> Holes { get; set; }
        public CreateHolesDto CreateHolesDto { get; set; }
    }
}
