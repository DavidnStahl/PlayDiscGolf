using PlayDiscGolf.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services
{
    public interface IAccountService
    {
        public Task<bool> UserLoggingIn(LoginViewModel model);

        public Task<bool> UserRegister(RegisterViewModel model);
    }
}
