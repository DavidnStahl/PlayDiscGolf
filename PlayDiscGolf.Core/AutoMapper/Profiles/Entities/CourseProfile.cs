using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.Entities
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<Course, SearchResultAjaxFormDto>()
            .ForMember(x => x.CourseID, map => map.MapFrom(x => x.CourseID))
            .ForMember(x => x.FullName, map => map.MapFrom(x => x.FullName))
            .ForMember(x => x.Area, map => map.MapFrom(x => x.Area))
            .ForMember(x => x.Holes, map => map.MapFrom(x => x.HolesTotal.ToString()));

        }
    }
}
