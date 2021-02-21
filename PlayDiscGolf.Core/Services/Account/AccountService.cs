using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Core.Dtos.Account;
using PlayDiscGolf.Core.Enums;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfwork _unitOfWork;

        public AccountService(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfwork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckIfCredentialsIsValidAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
  
            return await _signInManager.UserManager.CheckPasswordAsync(user, password);
        }

        public async Task<RegisterUserDto> UserRegisterAsync(RegisterDto model)
        {
            var ErrorMessegeEmaiResult = await IsEmailTakenAsync(model.Email);
            var ErrorMessegeUsernameResult = await IsUserNameTakenAsync(model.Username);

            var registerUserDto = new RegisterUserDto
            {
                ErrorMessegeEmail = ErrorMessegeEmaiResult,
                ErrorMessegeUsername = ErrorMessegeUsernameResult,
                CreateUserSucceded = !ErrorMessegeEmaiResult & !ErrorMessegeUsernameResult
            };

            if(registerUserDto.CreateUserSucceded == true)
            {
                return await CreateUserAsync(registerUserDto, model);
            }

            return registerUserDto;
        }

        private async Task<RegisterUserDto> CreateUserAsync(RegisterUserDto registerUserDto, RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return await SignInUserAfterRegisterAsync(user, registerUserDto);
            }

            return registerUserDto;
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

        private async Task<RegisterUserDto> SignInUserAfterRegisterAsync(IdentityUser user, RegisterUserDto registerUserDtos)
        {
            await _signInManager.SignInAsync(user, isPersistent: true);
            await _userManager.AddToRoleAsync(user, "User");
            registerUserDtos.CreateUserSucceded = true;

            return registerUserDtos;
        }      

        public async Task<bool> IsEmailTakenAsync(string email) =>
            await _userManager.FindByEmailAsync(email) != null;

        public async Task<bool> IsUserNameTakenAsync(string userName) =>
            await _userManager.FindByNameAsync(userName) != null;

        public async Task ChangePasswordAsync(string newPassword)
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

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
            user.UserName = newUserName;
            user.NormalizedUserName = newUserName;
            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                var scoreCards = _unitOfWork.ScoreCards.GetAllScoreCardAndIncludePlayerCardAndHoleCardBy(x => x.UserID == user.Id);

                foreach (var scoreCard in scoreCards)
                {
                    scoreCard.UserName = newUserName;
                    scoreCard.PlayerCards.FirstOrDefault(x => x.UserID == user.Id).UserName = newUserName;
                    _unitOfWork.ScoreCards.Edit(scoreCard);

                }

                _unitOfWork.Complete();               
            }
        }

        public async Task<IdentityUser> GetUserByQueryAsync(string query)
        {
            var user = await _userManager.FindByNameAsync(query);

            return user;
        }

        public string GetUserID()
        {
            return _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        }

        public async Task<IdentityUser> GetUserByIDAsync(string userID)
        {
            return await _userManager.FindByIdAsync(userID);
        }
    }
}
