using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.User
{
    public class FriendViewModel
    {
        public string FriendID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string FriendUserID { get; set; }
        public bool FriendRequestAccepted { get; set; }

        public UserInformationViewModel UserInformationViewModel = new UserInformationViewModel();
    }
}
