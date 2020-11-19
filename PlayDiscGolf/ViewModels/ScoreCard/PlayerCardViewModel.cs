using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.ScoreCard
{
    public class PlayerCardViewModel
    {
        public string PlayerCardID { get; set; }

        [Display(Name = "Player")]
        public string UserName { get; set; }
        public int TotalScore { get; set; }
        public string UserID { get; set; }

        public string ScoreCardID { get; set; }

        public List<HoleCardViewModel> HoleCards { get; set; }
    }
}
