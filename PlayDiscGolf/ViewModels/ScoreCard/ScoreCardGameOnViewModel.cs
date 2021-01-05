using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.Course;

namespace PlayDiscGolf.ViewModels.ScoreCard
{
    public class ScoreCardGameOnViewModel
    {
        public ScoreCardViewModel ScoreCard { get; set; }
        public HoleViewModel Hole { get; set; }
    }
}
