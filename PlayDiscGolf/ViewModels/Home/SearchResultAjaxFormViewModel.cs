using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.Home
{
    public class SearchResultAjaxFormViewModel
    {
        public Guid CourseID { get; set; }
        public string FullName { get; set; }

        public string Area { get; set; }

        public string Holes { get; set; }
    }
}
