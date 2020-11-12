using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.DataModels
{
    public class Hole
    {
        [Key]
        public int HoleID { get; set; }
        [Required]
        public int HoleNumber { get; set; }
        [Required]
        public int ParValue { get; set; }
        [Required]
        public int Distance { get; set; }

        [Display(Name = "Course")]
        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
    }
}
