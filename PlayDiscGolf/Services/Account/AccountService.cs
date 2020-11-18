using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels.Account;
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
            var registerUserDtos = new RegisterUserDto();

            registerUserDtos = await CheckIfEmailIsTaken(model, registerUserDtos);
            registerUserDtos = await CheckIfUsernameIsTaken(model, registerUserDtos);

            if (registerUserDtos.CreateUserSucceded == true)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return await CreateUserAsync(registerUserDtos, user);
            }            

            return registerUserDtos;
        }

        public async Task<RegisterUserDto> CreateUserAsync(RegisterUserDto registerUserDtos, IdentityUser user)
        {
            await _signInManager.SignInAsync(user, isPersistent: true);
            var allRoles = (new DataBaseContext()).Roles.OrderBy(r => r.Name).ToList();
            var role = allRoles.FirstOrDefault(r => r.Name == "User");
            _userManager.AddToRoleAsync(user, role.Name.ToString()).Wait();
            registerUserDtos.CreateUserSucceded = true;

            return registerUserDtos;
        }

        public async Task<RegisterUserDto> CheckIfEmailIsTaken(RegisterViewModel model,RegisterUserDto registerUserDtos)
        {
            var checkEmail = await _userManager.FindByEmailAsync(model.Email);

            if (checkEmail != null)
            {
                registerUserDtos.ErrorMessegeEmail = true;
                return registerUserDtos;
            }

            return registerUserDtos;
        }

        public async Task<RegisterUserDto> CheckIfUsernameIsTaken(RegisterViewModel model, RegisterUserDto registerUserDtos)
        {
            var checkUserName = await _userManager.FindByNameAsync(model.Username);

            if (checkUserName != null)
            {
                registerUserDtos.ErrorMessegeUsername = true;
                return registerUserDtos;
            }

            return registerUserDtos;
        }

        public async Task<string> GetInloggedUserID()
        {
            var user =  await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            return user.Id;
        }
    }
}
