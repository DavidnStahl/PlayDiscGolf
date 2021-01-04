using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.Cards
{
    public class HoleCardDto
    {
        public Guid HoleCardID { get; set; }
        public int HoleNumber { get; set; }
        public int Score { get; set; }
        public Guid PlayerCardID { get; set; }
        public PlayerCardDto PlayerCard = null;
    }
}
