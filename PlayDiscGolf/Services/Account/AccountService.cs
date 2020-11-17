using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.Models.Dtos;
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

        public async Task<RegisterUserDtos> UserRegister(RegisterViewModel model)
        {
            var registerUserDtos = new RegisterUserDtos();

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

        public async Task<RegisterUserDtos> CreateUserAsync(RegisterUserDtos registerUserDtos, IdentityUser user)
        {
            await _signInManager.SignInAsync(user, isPersistent: true);
            var allRoles = (new DataBaseContext()).Roles.OrderBy(r => r.Name).ToList();
            var role = allRoles.FirstOrDefault(r => r.Name == "User");
            _userManager.AddToRoleAsync(user, role.Name.ToString()).Wait();
            registerUserDtos.CreateUserSucceded = true;

            return registerUserDtos;
        }

        public async Task<RegisterUserDtos> CheckIfEmailIsTaken(RegisterViewModel model,RegisterUserDtos registerUserDtos)
        {
            var checkEmail = await _userManager.FindByEmailAsync(model.Email);

            if (checkEmail != null)
            {
                registerUserDtos.ErrorMessegeEmail = true;
                return registerUserDtos;
            }

            return registerUserDtos;
        }

        public async Task<RegisterUserDtos> CheckIfUsernameIsTaken(RegisterViewModel model, RegisterUserDtos registerUserDtos)
        {
            var checkUserName = await _userManager.FindByNameAsync(model.Username);

            if (checkUserName != null)
            {
                registerUserDtos.ErrorMessegeUsername = true;
                return registerUserDtos;
            }

            return registerUserDtos;
        }
    }
}
