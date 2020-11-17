using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.DataModels
{
    public class ScoreCard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ScoreCardID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 450)]
        public string UserID { get; set; }

        [ForeignKey("CourseID")]
        public virtual Guid CourseID { get; set; }

        
        public virtual Course Course { get; set; }

        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
    }
}
