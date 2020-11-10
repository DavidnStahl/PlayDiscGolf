using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models
{
    public class HoleCard
    {
        [Key]
        public int HoleCardID { get; set; }
        [Required]
        public int HoleNumber { get; set; }
        [Required]
        public int Score { get; set; }

        [Display(Name = "PlayerCard")]
        public int PlayerCardID { get; set; }

        [ForeignKey("PlayerCardID")]
        public virtual PlayerCard PlayerCard { get; set; }
    }
}
