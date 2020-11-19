using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.ScoreCard
{
    public class ScoreCardGameOnViewModel
    {
        public ScoreCardViewModel ScoreCardViewModel { get; set; }
        public Hole Hole { get; set; }
        public PagingViewModel PagingViewModel = new PagingViewModel();
    }
}
