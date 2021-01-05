using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.ViewModels.User;

namespace PlayDiscGolf.AutoMapper.Profiles.User
{
    public class UserUpdateInformationProfiles : Profile
    {
        public UserUpdateInformationProfiles()
        {
            CreateMap<UserUpdateInformationViewModel, UserUpdateInformationDto>();
            CreateMap<UserUpdateInformationDto, UserUpdateInformationViewModel>();
        }
    }
}
