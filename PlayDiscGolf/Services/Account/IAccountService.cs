using PlayDiscGolf.Models.Dtos;
using PlayDiscGolf.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services
{
    public interface IAccountService
    {
        public Task<RegisterUserDtos> UserRegister(RegisterViewModel model);
    }
}
