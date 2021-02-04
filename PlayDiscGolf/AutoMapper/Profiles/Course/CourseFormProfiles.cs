using AutoMapper;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PlayDiscGolf.Models.ViewModels.CourseFormViewModel;

namespace PlayDiscGolf.AutoMapper.Profiles.AdminCourse
{
    public class CourseFormProfiles : Profile
    {
        public CourseFormProfiles()
        {
            CreateMap<CourseFormViewModel, CourseFormDto>()
                .ForMember(x => x.ApiID, source => source.MapFrom(x => x.ApiID))
                .ForMember(x => x.ApiParentID, source => source.MapFrom(x => x.ApiParentID))
                .ForMember(x => x.Longitude, source => source.MapFrom(x => x.Longitude))
                .ForMember(x => x.Latitude, source => source.MapFrom(x => x.Latitude))
                .ForMember(x => x.Area, source => source.MapFrom(x => x.Area))
                .ForMember(x => x.Area, source => source.MapFrom(x => x.Area))
                .ForMember(x => x.Area, source => source.MapFrom(x => x.Area))
                .ForMember(x => x.Area, source => source.MapFrom(x => x.Area))
                .ForMember(x => x.CourseID, source => source.MapFrom(x => x.CourseID))
                .ForMember(x => x.CreateHoles, source => source.MapFrom(x => x.CreateHoles))
                .ForMember(x => x.FullName, source => source.MapFrom(x => x.FullName))
                .ForMember(x => x.Holes, source => source.MapFrom(x => x.Holes))
                .ForMember(x => x.HolesTotal, source => source.MapFrom(x => x.HolesTotal))
                .ForMember(x => x.Main, source => source.MapFrom(x => x.Main))
                .ForMember(x => x.Name, source => source.MapFrom(x => x.Name))
                .ForMember(x => x.NumberOfHoles, source => source.MapFrom(x => x.NumberOfHoles))
                .ForMember(x => x.TotalDistance, source => source.MapFrom(x => x.TotalDistance))
                .ForMember(x => x.TotalParValue, source => source.MapFrom(x => x.TotalParValue))
                .ForAllOtherMembers(x => x.Ignore());


            CreateMap<CourseFormDto, CourseFormViewModel>();
            CreateMap<CourseHolesViewModel, HoleDto>()
                .ForMember(x => x.Distance, source => source.MapFrom(x => x.Distance))
                .ForMember(x => x.HoleID, source => source.MapFrom(x => x.HoleID))
                .ForMember(x => x.HoleNumber, source => source.MapFrom(x => x.HoleNumber))
                .ForMember(x => x.ParValue, source => source.MapFrom(x => x.ParValue))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CourseHolesViewModel, CourseHolesDto>()
                .ForMember(x => x.Distance, source => source.MapFrom(x => x.Distance))
                .ForMember(x => x.HoleID, source => source.MapFrom(x => x.HoleID))
                .ForMember(x => x.HoleNumber, source => source.MapFrom(x => x.HoleNumber))
                .ForMember(x => x.ParValue, source => source.MapFrom(x => x.ParValue))
                .ForMember(x => x.CourseID, source => source.MapFrom(x => x.CourseID))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
