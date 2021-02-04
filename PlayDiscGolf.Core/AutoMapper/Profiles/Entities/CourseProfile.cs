using AutoMapper;
using PlayDiscGolf.Core.Dtos.AdminCourse;
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
            CreateMap<Course, CourseFormDto>()
                .ForMember(x => x.ApiID, map => map.MapFrom(x => x.ApiID))
                .ForMember(x => x.ApiParentID, map => map.MapFrom(x => x.ApiParentID))
                .ForMember(x => x.Area, map => map.MapFrom(x => x.Area))
                .ForMember(x => x.CountryCode, map => map.MapFrom(x => x.CountryCode))
                .ForMember(x => x.CourseID, map => map.MapFrom(x => x.CourseID))
                .ForMember(x => x.HolesTotal, map => map.MapFrom(x => x.HolesTotal))
                .ForMember(x => x.Latitude, map => map.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, map => map.MapFrom(x => x.Longitude))
                .ForMember(x => x.Main, map => map.MapFrom(x => x.Main))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.NumberOfHoles, map => map.MapFrom(x => x.HolesTotal))
                .ForMember(x => x.TotalDistance, map => map.MapFrom(x => x.TotalDistance))
                .ForMember(x => x.HolesTotal, map => map.MapFrom(x => x.HolesTotal))
                .ForMember(x => x.TotalParValue, map => map.MapFrom(x => x.TotalParValue))
                .ForMember(x => x.FullName, map => map.MapFrom(x => x.FullName))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CourseFormDto, Course>()
                .ForMember(x => x.ApiID, map => map.MapFrom(x => x.ApiID))
                .ForMember(x => x.ApiParentID, map => map.MapFrom(x => x.ApiParentID))
                .ForMember(x => x.Area, map => map.MapFrom(x => x.Area))
                .ForMember(x => x.CountryCode, map => map.MapFrom(x => x.CountryCode))
                .ForMember(x => x.CourseID, map => map.MapFrom(x => x.CourseID))
                .ForMember(x => x.HolesTotal, map => map.MapFrom(x => x.HolesTotal))
                .ForMember(x => x.Latitude, map => map.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, map => map.MapFrom(x => x.Longitude))
                .ForMember(x => x.Main, map => map.MapFrom(x => x.Main))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.HolesTotal, map => map.MapFrom(x => x.NumberOfHoles))
                .ForMember(x => x.TotalDistance, map => map.MapFrom(x => x.TotalDistance))
                .ForMember(x => x.HolesTotal, map => map.MapFrom(x => x.HolesTotal))
                .ForMember(x => x.TotalParValue, map => map.MapFrom(x => x.TotalParValue))
                .ForMember(x => x.FullName, map => map.MapFrom(x => x.FullName))
                .ForAllOtherMembers(x => x.Ignore());




        }
    }
}
