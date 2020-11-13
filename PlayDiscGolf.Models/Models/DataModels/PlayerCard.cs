﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.DataModels
{
    public class PlayerCard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PlayerCardID { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [ForeignKey("ScoreCardID")]
        public Guid ScoreCardID { get; set; }

        
        public ScoreCard Scorecard { get; set; }

        public virtual ICollection<HoleCard> HoleCards { get; set; }
    }
}
