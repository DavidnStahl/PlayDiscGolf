using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.Models.ViewModels.Account;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountService(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> UserLoggingIn(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UserRegister(RegisterViewModel model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                var allRoles = (new DataBaseContext()).Roles.OrderBy(r => r.Name).ToList();
                var role = allRoles.FirstOrDefault(r => r.Name == "User");
                _userManager.AddToRoleAsync(user, role.Name.ToString()).Wait();

                return true;
            }

            return false;
        }
    }
}
