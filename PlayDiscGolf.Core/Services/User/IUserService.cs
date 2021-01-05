
using PlayDiscGolf.Core.Dtos.User;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.User
{
    public interface IUserService
    {
        Task<UserInformationDto> GetUserInformationAsync();
    }
}
