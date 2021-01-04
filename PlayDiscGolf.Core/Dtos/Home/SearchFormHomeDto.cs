using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlayDiscGolf.Core.Dtos.Home
{
    public class SearchFormHomeDto
    {
        public string Type { get; set; }

        public string Country { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Query { get; set; }

        public List<SelectListItem> Types { get; set; }

        [Display(Name = "Choose country")]
        public List<SelectListItem> Countries { get; set; }

        public List<SearchResultAjaxFormDto> SearchResultAjaxFormViewModel = new List<SearchResultAjaxFormDto>();
    }
}
