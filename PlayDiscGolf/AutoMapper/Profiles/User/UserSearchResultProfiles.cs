using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.ViewModels.User;

namespace PlayDiscGolf.AutoMapper.Profiles.User
{
    public class UserSearchResultProfiles : Profile
    {
        public UserSearchResultProfiles()
        {
            CreateMap<UserSearchResultViewModel, UserSearchResultDto>();
            CreateMap<UserSearchResultDto, UserSearchResultViewModel>();
        }
    }
}
