using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.ScoreCard
{
    public class HoleCardViewModel
    {
        public Guid HoleCardID { get; set; }
        public int HoleNumber { get; set; }
        public int Score { get; set; }
        public Guid PlayerCardID { get; set; }
    }
}
