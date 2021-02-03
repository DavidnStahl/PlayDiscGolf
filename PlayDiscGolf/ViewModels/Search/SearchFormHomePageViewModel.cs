using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.Home
{
    public class SearchFormHomePageViewModel
    {
        public string Type { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Query { get; set; }
        public List<SelectListItem> Types = new List<SelectListItem>();
        [Display(Name = "Choose country")]
        public List<SelectListItem> Countries = new List<SelectListItem>();
        public List<SearchResultAjaxFormViewModel> SearchResultAjaxFormViewModel = new List<SearchResultAjaxFormViewModel>();
    }
}
