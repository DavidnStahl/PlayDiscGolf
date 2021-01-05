using AutoMapper;
using PlayDiscGolf.Core.Dtos.Account;
using PlayDiscGolf.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.Account
{
    public class RegisterProfiles : Profile
    {
        public RegisterProfiles()
        {
            CreateMap<RegisterViewModel, RegisterDto>();
            CreateMap<RegisterDto, RegisterViewModel>();
        }
    }
}
