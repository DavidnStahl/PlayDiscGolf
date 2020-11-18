using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Dtos
{
    public class ScoreCardDto
    {
        public Guid ScoreCardID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }

        public string  CourseID { get; set; }

        public List<PlayerCard> PlayerCards { get; set; }
    }
}
