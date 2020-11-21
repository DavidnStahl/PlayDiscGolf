using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PlayDiscGolf.Models.ViewModels.Account;
using PlayDiscGolf.Services;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Dtos;

namespace PlayDiscGolf.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccountService _accountService;

        public AccountController(SignInManager<IdentityUser> signInManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login() => 
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded) return RedirectToAction("index", "home");

            return View(model);            
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        public IActionResult Register() =>
            View(new RegisterViewModel());
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            RegisterUserDto registerUserDtos = await _accountService.UserRegisterAsync(model);
            if (registerUserDtos.CreateUserSucceded == true) return RedirectToAction("Index", "Home");
            if(registerUserDtos.ErrorMessegeEmail == true) ModelState.AddModelError("Email", "Email is taken");  
            if(registerUserDtos.ErrorMessegeUsername == true) ModelState.AddModelError("Username", "Username is taken");           

            return View(model);
        }
    }
        
    
}
