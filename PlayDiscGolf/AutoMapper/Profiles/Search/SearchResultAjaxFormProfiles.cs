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
            CreateMap<CourseDto, SearchResultAjaxFormViewModel>()
                .ForMember(x => x.Holes, source => source.MapFrom(x => x.TotalHoles))
                .ForMember(x => x.Area, source => source.MapFrom(x => x.Area))
                .ForMember(x => x.FullName, source => source.MapFrom(x => x.FullName))
                .ForMember(x => x.CourseID, source => source.MapFrom(x => x.CourseID))
                .ForAllOtherMembers(x => x.Ignore());

        }
    }
}
