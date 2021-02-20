using Microsoft.AspNetCore.Mvc.Rendering;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
using PlayDiscGolf.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels
{
    public class AdminSearchViewModel
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public string Query { get; set; }
        public List<SelectListItem> Types = new List<SelectListItem>();
        public List<SelectListItem> Countries = new List<SelectListItem>();
        public CourseFormViewModel Course { get; set; }
        public List<SearchResultAjaxFormViewModel> SearchResultAjaxFormViewModel = new List<SearchResultAjaxFormViewModel>();

    }
}
