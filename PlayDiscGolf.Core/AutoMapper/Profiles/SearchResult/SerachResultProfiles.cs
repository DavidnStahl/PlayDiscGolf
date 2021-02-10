using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.SearchResult
{
    public class SerachResultProfiles : Profile
    {
        public SerachResultProfiles()
        {
            CreateMap<CourseDto, SearchResultAjaxFormDto>()
                .ForMember(x => x.Holes, source => source.MapFrom(x => x.TotalHoles.ToString()))
                .ForMember(x => x.FullName, source => source.MapFrom(x => x.FullName))
                .ForMember(x => x.CourseID, source => source.MapFrom(x => x.CourseID))
                .ForMember(x => x.Area, source => source.MapFrom(x => x.Area));


        }
    }    
}
