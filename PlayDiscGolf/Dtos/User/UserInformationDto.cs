using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Dtos.User
{
    public class UserInformationDto
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string SearchUsername { get; set; }
        public string YourNameInThoseGames { get; set; }
        public List<PlayerCardDto> PlayerCards { get; set; }
        public bool ClaimGames { get; set; }
    }
}
