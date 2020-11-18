using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.Models.DataModels
{
    public class Hole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid HoleID { get; set; }
        [Required]
        public int HoleNumber { get; set; }
        [Required]
        public int ParValue { get; set; }
        [Required]
        public int Distance { get; set; }

        [ForeignKey("CourseID")]
        public Guid CourseID { get; set; }

        
        public virtual Course Course { get; set; }
    }
}
