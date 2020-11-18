using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
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

        public CourseFormViewModel Course = new CourseFormViewModel();


        public List<SearchCourseItemViewModel> SearchCourseItemViewModels = new List<SearchCourseItemViewModel>();

    }
}
