using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models
{
    public class PlayerCard
    {
        [Key]
        public int PlayerCardID { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Display(Name = "Scorecard")]
        public int ScoreCardID { get; set; }

        [ForeignKey("ScoreCardID")]
        public ScoreCard Scorecard { get; set; }

        public virtual ICollection<HoleCard> HoleCards { get; set; }
    }
}
