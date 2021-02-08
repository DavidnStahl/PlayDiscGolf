using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.Dtos.User
{
    public class FriendDto
    {
        public Guid FriendID { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public Guid FriendUserID { get; set; }
        public bool FriendRequestAccepted { get; set; }
    }
}
