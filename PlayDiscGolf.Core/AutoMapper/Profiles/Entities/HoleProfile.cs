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
        }
    }
}
