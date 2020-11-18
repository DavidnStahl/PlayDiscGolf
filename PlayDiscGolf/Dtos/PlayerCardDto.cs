using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Dtos
{
    public class PlayerCardDto
    {
        public Guid PlayerCardID { get; set; }
        public string UserName { get; set; }

        public string UserID { get; set; }

        public Guid ScoreCardID { get; set; }

        public List<HoleCard> HoleCards { get; set; }
    }
}
