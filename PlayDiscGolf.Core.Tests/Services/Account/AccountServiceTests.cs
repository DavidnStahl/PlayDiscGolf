using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using PlayDiscGolf.Core.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PlayDiscGolf.Core.Tests.Services.Account
{
    public class AccountServiceTests
    {

        [Fact]
        public async Task CreateUserAsync_When_CreateAsync_Return_True()
        {

            //Arange
            var model = new RegisterDto
            {
                Username = "David",
                Password = "Hejsan123!",
                Email = "david@gmail.com",
                ConfirmPassword = "Hejsan123!"
            };

            var store = new Mock<UserManager<IdentityUser>>();

            var user = new IdentityUser
            {
                UserName = "David",
                Email = "davidstahl@gmail.com"
            };


            store.Setup(s =>
                s.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();


            //Act
            user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await store.Object.CreateAsync(user, model.Password);

            //assert
            Assert.True(result.Succeeded);
        }

        /*public Task<RegisterUserDto> UserRegisterAsync(RegisterDto model);

        public Task<string> GetInloggedUserIDAsync();

        Task<IdentityUser> GetUserByQueryAsync(string query);

        Task<IdentityUser> GetUserByIDAsync(string userID);

        Task<string> GetEmailAsync();

        string GetUserName();

        Task<bool> CheckIfCredentialsIsValidAsync(string username, string password);


        Task ChangePasswordAsync(string newPassword);

        Task ChangeEmailAsync(string newEmail);

        Task ChangeUserNameAsync(string newUserName);
        Task<bool> IsEmailTakenAsync(string email);
        Task<bool> IsUserNameTakenAsync(string userName);*/
    }
}
