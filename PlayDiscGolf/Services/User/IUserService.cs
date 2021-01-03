using PlayDiscGolf.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.User
{
    public interface IUserService
    {
        Task<UserInformationViewModel> GetUserInformationAsync();
    }
}
