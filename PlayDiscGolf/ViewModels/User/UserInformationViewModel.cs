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
        public string YourNameInThoseGames { get; set; }
        public List<PlayerCardViewModel> PlayerCards { get; set; }
        public bool ClaimGames { get; set; }
    }
}
