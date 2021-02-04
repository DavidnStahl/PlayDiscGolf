using AutoMapper;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.Entities
{
    public class HoleProfile : Profile
    {
        public HoleProfile()
        {
            CreateMap<Hole, HoleDto>();
            CreateMap<HoleDto, Hole>();
            CreateMap<Hole, CourseHolesDto>();
            CreateMap<CourseHolesDto, Hole>()
                .ForMember(x => x.Distance, source => source.MapFrom(x => x.Distance))
                .ForMember(x => x.HoleNumber, source => source.MapFrom(x => x.HoleNumber))
                .ForMember(x => x.ParValue, source => source.MapFrom(x => x.ParValue))
                .ForMember(x => x.HoleID, source => source.MapFrom(x => x.HoleID))
                .ForMember(x => x.CourseID, source => source.MapFrom(x => x.CourseID))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
