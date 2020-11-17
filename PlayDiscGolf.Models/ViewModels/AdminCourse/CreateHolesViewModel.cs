using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static PlayDiscGolf.Models.ViewModels.CourseFormViewModel;

namespace PlayDiscGolf.Models.ViewModels
{
    public class CreateHolesViewModel
    {
        public int NumberOfHoles { get; set; }
        public Guid CourseID { get; set; }

        public List<CourseHolesViewModel> Holes = new List<CourseHolesViewModel>();       
    }
}
