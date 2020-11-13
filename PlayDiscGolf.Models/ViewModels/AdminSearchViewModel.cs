using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels
{
    public class AdminSearchViewModel
    {
        [Display(Name = "Search location")]
        public string Query { get; set; }

        public List<CourseFormViewModel> Courses = new List<CourseFormViewModel>();

        public Course Course { get; set; }

        public List<SearchLocationItemViewModel> SearchLocationItemViewModels = new List<SearchLocationItemViewModel>();

    }
}
