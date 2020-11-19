using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels.Account;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
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

        public async Task<RegisterUserDto> UserRegister(RegisterViewModel model)
        {
            RegisterUserDto registerUserDtos = await CheckIfEmailIsTaken(model, new RegisterUserDto());

            registerUserDtos = await CheckIfUsernameIsTaken(model, registerUserDtos);

            return registerUserDtos.CreateUserSucceded == true ? await CreateUserAsync(registerUserDtos, model) : registerUserDtos;
        }
        public async Task<string> GetInloggedUserID()
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            return user.Id;
        }

        private async Task<RegisterUserDto> CreateUserAsync(RegisterUserDto registerUserDtos, RegisterViewModel model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Username, Email = model.Email };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            return (result.Succeeded)? await SignInUserAfterRegister(user, registerUserDtos) : registerUserDtos;
        }

        private async Task<RegisterUserDto> SignInUserAfterRegister(IdentityUser user, RegisterUserDto registerUserDtos)
        {
            await _signInManager.SignInAsync(user, isPersistent: true);

            _userManager.AddToRoleAsync(user, (new DataBaseContext()).Roles.OrderBy(r => r.Name).FirstOrDefault(r => r.Name == "User").Name.ToString()).Wait();

            registerUserDtos.CreateUserSucceded = true;

            return registerUserDtos;
        }

        private async Task<RegisterUserDto> CheckIfEmailIsTaken(RegisterViewModel model,RegisterUserDto registerUserDtos)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null) registerUserDtos.ErrorMessegeEmail = true;

            return registerUserDtos;
        }

        private async Task<RegisterUserDto> CheckIfUsernameIsTaken(RegisterViewModel model, RegisterUserDto registerUserDtos)
        {
            if (await _userManager.FindByNameAsync(model.Username) != null) registerUserDtos.ErrorMessegeUsername = true;

            return registerUserDtos;
        }     
    }
}
