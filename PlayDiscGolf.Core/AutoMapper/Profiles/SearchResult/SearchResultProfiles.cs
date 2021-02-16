using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.SearchResult
{
    public class SearchResultProfiles : Profile
    {
        public SearchResultProfiles()
        {
            CreateMap<Course, SearchResultAjaxFormDto>()
                .ForMember(x => x.Holes, source => source.MapFrom(x => x.HolesTotal.ToString()))
                .ForMember(x => x.FullName, source => source.MapFrom(x => x.FullName))
                .ForMember(x => x.CourseID, source => source.MapFrom(x => x.CourseID))
                .ForMember(x => x.Area, source => source.MapFrom(x => x.Area));


        }
    }    
}
