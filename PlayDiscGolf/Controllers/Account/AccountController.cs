using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PlayDiscGolf.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Core.Services.Account;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Account;

namespace PlayDiscGolf.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<IdentityUser> signInManager, IAccountService accountService, IMapper mapper)
        {
            _signInManager = signInManager;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login() => 
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var validCredentials = await _accountService.CheckIfCredentialsIsValidAsync(model.UserName, model.Password);

            if(!validCredentials)
            {
                ModelState.AddModelError("Username","Username or password is wrong");
                ModelState.AddModelError("Password", "Password or username wrong");

                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded) 
                return RedirectToAction("index", "home");

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
            if (!ModelState.IsValid) 
                return View(model);

            var registerUserDtos = await _accountService.UserRegisterAsync(_mapper.Map<RegisterDto>(model));

            if (registerUserDtos.CreateUserSucceded == true) 
                return RedirectToAction("Index", "Home");

            if(registerUserDtos.ErrorMessegeEmail == true) 
                ModelState.AddModelError("Email", "Email is taken");
            
            if(registerUserDtos.ErrorMessegeUsername == true) 
                ModelState.AddModelError("Username", "Username is taken");           

            return View(model);
        }
    }
        
    
}
