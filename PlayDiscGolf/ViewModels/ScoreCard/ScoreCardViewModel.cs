using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.ScoreCard
{
    public class ScoreCardViewModel
    {
        
        public string ScoreCardID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Display(Name = "Creater")]
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string CourseID { get; set; }
        public List<PlayerCardViewModel> PlayerCards { get; set; } = new List<PlayerCardViewModel>();
    }
}
