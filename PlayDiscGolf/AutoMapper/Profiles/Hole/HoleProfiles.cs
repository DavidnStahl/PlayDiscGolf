using AutoMapper;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PlayDiscGolf.Models.ViewModels.CourseFormViewModel;

namespace PlayDiscGolf.AutoMapper.Profiles.Hole
{
    public class HoleProfiles : Profile
    {
        public HoleProfiles()
        {
            CreateMap<HoleViewModel, HoleDto>();
            CreateMap<HoleDto, HoleViewModel>()
                .ForMember(x => x.Distance, source => source.MapFrom(x => x.Distance))
                .ForMember(x => x.HoleID, source => source.MapFrom(x => x.HoleID))
                .ForMember(x => x.HoleNumber, source => source.MapFrom(x => x.HoleNumber))
                .ForMember(x => x.ParValue, source => source.MapFrom(x => x.ParValue))
                .ForAllOtherMembers(x => x.Ignore());
            

            CreateMap<HoleDto, CourseHolesViewModel>();
            CreateMap<CourseHolesViewModel, HoleDto>()
                .ForMember(x => x.Distance, source => source.MapFrom(x => x.Distance))
                .ForMember(x => x.HoleID, source => source.MapFrom(x => x.HoleID))
                .ForMember(x => x.HoleNumber, source => source.MapFrom(x => x.HoleNumber))
                .ForMember(x => x.ParValue, source => source.MapFrom(x => x.ParValue))
                .ForAllMembers(x => x.Ignore());
            
        }
    }
}
