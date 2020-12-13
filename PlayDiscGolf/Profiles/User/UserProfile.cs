using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Dtos.User;
using PlayDiscGolf.ViewModels.User;

namespace PlayDiscGolf.Profiles.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInformationViewModel, UserInformationDto>();
            CreateMap<UserInformationDto, UserInformationViewModel>();
        }
    }
}
