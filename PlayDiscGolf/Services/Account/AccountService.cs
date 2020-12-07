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
            RegisterUserDto registerUserDtos = await CheckIfEmailIsTakenAsync(model, new RegisterUserDto());
            registerUserDtos = await CheckIfUsernameIsTakenAsync(model, registerUserDtos);

            return registerUserDtos.CreateUserSucceded == true ? await CreateUserAsync(registerUserDtos, model) : registerUserDtos;
        }
        public async Task<string> GetInloggedUserID()
        {
            IdentityUser user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            return user.Id;
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

        private async Task<RegisterUserDto> CheckIfEmailIsTakenAsync(RegisterViewModel model,RegisterUserDto registerUserDtos)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null) 
                registerUserDtos.ErrorMessegeEmail = true;

            return registerUserDtos;
        }

        private async Task<RegisterUserDto> CheckIfUsernameIsTakenAsync(RegisterViewModel model, RegisterUserDto registerUserDtos)
        {
            if (await _userManager.FindByNameAsync(model.Username) != null) 
                registerUserDtos.ErrorMessegeUsername = true;

            return registerUserDtos;
        }     
    }
}
