
using PlayDiscGolf.Core.Dtos.ScoreCard;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.User
{
    public interface IUserService
    {
        Task<UserInformationDto> GetUserInformationAsync();
    }
}
