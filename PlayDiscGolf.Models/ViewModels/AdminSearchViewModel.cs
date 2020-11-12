using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels
{
    public class AdminSearchViewModel
    {
        [Display(Name = "Query")]
        public string Query { get; set; }
        public List<SearchLocationItemViewModel> SearchLocationItemViewModels { get; set; }
    }
}
