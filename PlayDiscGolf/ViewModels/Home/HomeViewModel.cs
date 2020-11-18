
using Microsoft.AspNetCore.Mvc.Rendering;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels
{
    public class HomeViewModel
    {
        public SearchFormHomePageViewModel SearchFormHomePageViewModel = new SearchFormHomePageViewModel();
    }
}
