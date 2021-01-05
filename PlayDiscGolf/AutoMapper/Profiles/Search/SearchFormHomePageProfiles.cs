using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.ViewModels.Home;

namespace PlayDiscGolf.AutoMapper.Profiles.Search
{
    public class SearchFormHomePageProfiles : Profile
    {
        public SearchFormHomePageProfiles()
        {

            CreateMap<SearchFormHomePageViewModel, SearchFormHomeDto>();
            CreateMap<SearchFormHomeDto, SearchFormHomePageViewModel>();
        }
    }
}
