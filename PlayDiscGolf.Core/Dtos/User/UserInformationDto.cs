using PlayDiscGolf.Core.Dtos.Cards;
using System.Collections.Generic;


namespace PlayDiscGolf.Core.Dtos.User
{
    public class UserInformationDto
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string SearchUsername { get; set; }
        public List<string> Friends { get; set; }

        public string SearchResult { get; set; }
    }
}
