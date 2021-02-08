
using PlayDiscGolf.Core.Dtos.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.User
{
    public interface IUserService
    {
        Task<string> SearchUsersAsync(string query);

        Task<List<FriendDto>> GetFriendsAsync();

        Task SendFriendRequestAsync(string username);

        Task RemoveFriendAsync(string username);

        Task<List<FriendDto>> GetFriendRequestsAsync();

        void AcceptFriendRequest(string friendID);

        void DeclineFriendRequest(string friendID);
    }
}
