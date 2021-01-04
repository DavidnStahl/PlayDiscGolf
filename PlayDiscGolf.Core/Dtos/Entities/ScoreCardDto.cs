using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.Cards
{
    public class ScoreCardDto
    {        
        public Guid ScoreCardID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public Guid CourseID { get; set; }
        public List<PlayerCardDto> PlayerCards = null;
    }
}
