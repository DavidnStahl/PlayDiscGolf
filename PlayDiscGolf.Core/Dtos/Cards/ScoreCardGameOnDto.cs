using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Core.Dtos.Cards
{
    public class ScoreCardGameOnDto
    {
        public ScoreCardDto ScoreCard { get; set; }
        public HoleDto Hole { get; set; }
    }
}
