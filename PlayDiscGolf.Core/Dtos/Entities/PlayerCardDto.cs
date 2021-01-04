using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.Cards
{
    public class PlayerCardDto
    {
        public Guid PlayerCardID { get; set; }
        public string UserName { get; set; }
        public int TotalScore { get; set; }
        public string UserID { get; set; }
        public Guid ScoreCardID { get; set; }
        public List<HoleCardDto> HoleCards = null;
    }
}
