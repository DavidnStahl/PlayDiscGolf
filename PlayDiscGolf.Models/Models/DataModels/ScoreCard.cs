using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.DataBaseModels
{
    public class ScoreCard
    {
        [Key]
        public int ScoreCardID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string UserName { get; set; }

        [Display(Name = "Course")]
        public virtual int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }

        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
    }
}
