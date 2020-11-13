using AutoMapper;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Profiles
{
    public class HoleProfile : Profile
    {
        public HoleProfile()
        {
            //Source -> Target
            CreateMap<Hole, CourseFormViewModel.CourseHolesViewModel>();
            CreateMap<CourseFormViewModel.CourseHolesViewModel, Hole>();
        }
    }
}
