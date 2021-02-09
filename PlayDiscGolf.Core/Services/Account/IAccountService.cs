using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Core.Dtos.Account;
using PlayDiscGolf.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.Account
{
    public interface IAccountService
    {
        public Task<RegisterUserDto> UserRegisterAsync(RegisterDto model);

        public Task<string> GetInloggedUserIDAsync();

        Task<IdentityUser> GetUserByQueryAsync(string query);

        Task<IdentityUser> GetUserByIDAsync(string userID);

        Task<string> GetEmailAsync();

        string GetUserName();

        Task ChangePasswordAsync(string newPassword);

        Task ChangeEmailAsync(string newEmail);

        Task ChangeUserNameAsync(string newUserName);
        Task<bool> IsEmailTakenAsync(string email);
        Task<bool> IsUserNameTakenAsync(string userName);
    }
}
