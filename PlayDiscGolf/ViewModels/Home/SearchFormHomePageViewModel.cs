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
        [Required(ErrorMessage = "Required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Query { get; set; }

        public List<SelectListItem> Types { get; set; }

        [Display(Name = "Choose country")]
        public List<SelectListItem> Countries { get; set; }

        public List<SearchResultAjaxFormViewModel> SearchResultAjaxFormViewModel = new List<SearchResultAjaxFormViewModel>();
    }
}
