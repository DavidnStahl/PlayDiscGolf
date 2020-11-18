using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.ScoreCard
{
    public class HoleCardViewModel
    {
        public string HoleCardID { get; set; }
        public int HoleNumber { get; set; }
        public int Score { get; set; }
        public string PlayerCardID { get; set; }
    }
}
