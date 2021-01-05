using AutoMapper;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.AdminCourse
{
    public class AdminSearchProfiles : Profile
    {
        public AdminSearchProfiles()
        {
            CreateMap<AdminSearchViewModel, AdminSearchDto>();
            CreateMap<AdminSearchDto, AdminSearchViewModel>();
        }
    }
}
