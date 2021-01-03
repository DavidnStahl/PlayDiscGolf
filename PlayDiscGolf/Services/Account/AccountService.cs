using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels.Account;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterUserDto> UserRegisterAsync(RegisterViewModel model)
        {
            var registerUserDto = new RegisterUserDto
            {
                ErrorMessegeEmail = await IsEmailTakenAsync(model.Email),
                ErrorMessegeUsername = await IsUserNameTakenAsync(model.Username)
            };

            return registerUserDto.CreateUserSucceded == true ? await CreateUserAsync(registerUserDto, model) : registerUserDto;
        }
        public async Task<string> GetInloggedUserIDAsync()
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            return user.Id;
        }

        public async Task<string> GetEmailAsync()
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            return user.Email;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
        private async Task<RegisterUserDto> CreateUserAsync(RegisterUserDto registerUserDtos, RegisterViewModel model)
        {
            var user = new IdentityUser 
            { 
                UserName = model.Username,
                Email = model.Email 
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return (result.Succeeded)? await SignInUserAfterRegisterAsync(user, registerUserDtos) : registerUserDtos;
        }

        private async Task<RegisterUserDto> SignInUserAfterRegisterAsync(IdentityUser user, RegisterUserDto registerUserDtos)
        {
            await _signInManager.SignInAsync(user, isPersistent: true);

            _userManager.AddToRoleAsync(user, (new DataBaseContext()).Roles.OrderBy(r => r.Name)
                .FirstOrDefault(r => r.Name == EnumHelper.UserManager.User.ToString()).Name.ToString()).Wait();

            registerUserDtos.CreateUserSucceded = true;

            return registerUserDtos;
        }      

        public async Task<bool> IsEmailTakenAsync(string email) =>
            await _userManager.FindByNameAsync(email) != null;

        public async Task<bool> IsUserNameTakenAsync(string userName) =>
            await _userManager.FindByNameAsync(userName) != null;

        public async Task ChangePasswordAsync(string newPassword)
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ChangePasswordAsync(user, token, newPassword);

            if(result.Succeeded)
                await _signInManager.RefreshSignInAsync(user);
        }

        public async Task ChangeEmailAsync(string newEmail)
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (result.Succeeded)
                await _signInManager.RefreshSignInAsync(user);
        }

        public async Task ChangeUserNameAsync(string newUserName)
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            user.NormalizedUserName = newUserName;
            await _userManager.UpdateNormalizedUserNameAsync(user);
        }

        public async Task<IdentityUser> GetUserByQueryAsync(string query)
        {
            var user = await _userManager.FindByNameAsync(query);

            return user;
        }
    }
}
