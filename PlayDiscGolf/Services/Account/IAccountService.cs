using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.ViewModels.Account;
using PlayDiscGolf.ViewModels.User;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services
{
    public interface IAccountService
    {
        public Task<RegisterUserDto> UserRegisterAsync(RegisterViewModel model);

        public Task<string> GetInloggedUserIDAsync();

        Task<string> GetEmailAsync();

        string GetUserName();

        Task ChangePasswordAsync(string newPassword);

        Task ChangeEmailAsync(string newEmail);

        Task ChangeUserNameAsync(string newUserName);
        Task<bool> IsEmailTakenAsync(string email);
        Task<bool> IsUserNameTakenAsync(string userName);



    }
}
