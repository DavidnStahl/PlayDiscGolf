using AutoMapper;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using System.Collections.Generic;

namespace PlayDiscGolf.Profiles
{
    public class HoleProfile : Profile
    {
        public HoleProfile()
        {
            CreateMap<Hole, CourseFormViewModel.CourseHolesViewModel>();
            CreateMap<CourseFormViewModel.CourseHolesViewModel, Hole>();

            CreateMap<CreateHolesViewModel, Hole>();
            CreateMap<Hole, CreateHolesViewModel>();

            CreateMap<List<HoleInfoDto>, List<Hole>>();
        }
    }
}
