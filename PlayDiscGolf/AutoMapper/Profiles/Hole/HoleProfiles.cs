using AutoMapper;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.Hole
{
    public class HoleProfiles : Profile
    {
        public HoleProfiles()
        {
            CreateMap<HoleViewModel, HoleDto>();
            CreateMap<HoleDto, HoleViewModel>();
        }
    }
}
