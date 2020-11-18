using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.Models.DataModels
{
    public class HoleCard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid HoleCardID { get; set; }
        [Required]
        public int HoleNumber { get; set; }
        [Required]
        public int Score { get; set; }

        [ForeignKey("PlayerCardID")]
        public Guid PlayerCardID { get; set; }

        
        public virtual PlayerCard PlayerCard { get; set; }
    }
}
