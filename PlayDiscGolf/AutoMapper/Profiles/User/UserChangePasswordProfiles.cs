using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.User
{
    public class UserChangePasswordProfiles : Profile
    {
        public UserChangePasswordProfiles()
        {
            CreateMap<UserChangePasswordViewModel, UserChangePasswordDto>();
        }
    }
}
