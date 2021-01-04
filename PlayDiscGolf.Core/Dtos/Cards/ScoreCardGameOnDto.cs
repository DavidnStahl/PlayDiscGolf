using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Core.Dtos.Cards
{
    public class ScoreCardGameOnDto
    {
        public ScoreCardDto ScoreCardViewModel { get; set; }
        public Hole Hole { get; set; }
    }
}
