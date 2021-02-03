using AutoMapper;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.AdminCourse
{
    public class CreateHolesProfiles : Profile
    {
        public CreateHolesProfiles()
        {
            CreateMap<CreateHolesViewModel, CreateHolesDto>();
            CreateMap<CreateHolesDto, CreateHolesViewModel>();
        }
    }
}
