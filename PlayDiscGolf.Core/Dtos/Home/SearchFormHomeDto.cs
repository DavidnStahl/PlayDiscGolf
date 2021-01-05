using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlayDiscGolf.Core.Dtos.Home
{
    public class SearchFormHomeDto
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public string Query { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Types { get; set; }
        public List<SearchResultAjaxFormDto> SearchResultAjaxFormDto = new List<SearchResultAjaxFormDto>();
    }
}
