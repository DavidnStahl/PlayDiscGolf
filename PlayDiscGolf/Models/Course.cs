using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        public virtual ICollection<ScoreCard> ScoreCards { get; set; }
    }
}
