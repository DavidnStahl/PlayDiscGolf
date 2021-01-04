using AutoMapper;
using PlayDiscGolf.Core.Dtos.PostModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.Search
{
    public class SearchProfiles : Profile
    {
        public SearchProfiles()
        {
            CreateMap<SearchViewModel, SearchDto>();
            CreateMap<SearchDto, SearchViewModel>();
        }
    }
}
