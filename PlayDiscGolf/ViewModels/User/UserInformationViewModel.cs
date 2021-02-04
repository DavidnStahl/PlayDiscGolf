using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.User
{
    public class UserInformationViewModel
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string SearchUsername { get; set; }
        public string SearchResult { get; set; }
        public List<string> Friends = new List<string>();

        public UserSearchResultViewModel UserSearchResultViewModel = new UserSearchResultViewModel();

        public UserChangeEmailViewModel UserChangeEmailViewModel = new UserChangeEmailViewModel();

    }
}
