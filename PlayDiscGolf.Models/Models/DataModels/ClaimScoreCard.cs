using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlayDiscGolf.Models.Models.DataModels
{
    public class ClaimScoreCard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ClaimID { get; set; }

        public Guid UserID { get; set; }

        public string NameToClaim { get; set; }

        public string ClaimingUsername { get; set; }

        public Guid ClamingUserID { get; set; }

        public bool ClaimAccepted { get; set; }

        public bool ClaimResponse { get; set; }

        public DateTime ClaimDate { get; set; }
    }
}
