using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.ViewModels.Home;

namespace PlayDiscGolf.AutoMapper.Profiles.Search
{
    public class SearchResultAjaxFormProfiles : Profile
    {
        public SearchResultAjaxFormProfiles()
        {
            CreateMap<SearchResultAjaxFormDto, SearchResultAjaxFormViewModel>();
        }
    }
}
