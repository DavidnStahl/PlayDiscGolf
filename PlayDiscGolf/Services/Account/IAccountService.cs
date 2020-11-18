using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.ViewModels.Account;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services
{
    public interface IAccountService
    {
        public Task<RegisterUserDto> UserRegister(RegisterViewModel model);

        public Task<string> GetInloggedUserID();
    }
}
