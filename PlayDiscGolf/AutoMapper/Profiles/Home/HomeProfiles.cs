using AutoMapper;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.Home
{
    public class HomeProfiles : Profile
    {
        public HomeProfiles()
        {
            CreateMap<HomeViewModel, HomeDto>();
            CreateMap<HomeDto, HomeViewModel>();
        }
    }
}
