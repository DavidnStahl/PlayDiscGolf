
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.DataBaseModels
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        
        public bool Main { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Required]
        public int HolesTotal { get; set; }

        [Required]
        public int TotalParValue { get; set; }

        public int TotalDistance { get; set; }

        [Display(Name = "Location")]
        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        public virtual ICollection<ScoreCard> ScoreCards { get; set; }

        public virtual ICollection<Hole> Holes { get; set; }
    }
}
