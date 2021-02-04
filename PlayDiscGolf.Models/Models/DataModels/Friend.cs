using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.Models.DataModels
{
    public class Friend
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FriendID { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public Guid FriendUserID { get; set; }
    }
}
