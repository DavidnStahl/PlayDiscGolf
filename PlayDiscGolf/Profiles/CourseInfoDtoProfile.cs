using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.ViewModels.Course;

namespace PlayDiscGolf.Profiles
{
    public class CourseInfoDtoProfile : Profile
    {
        public CourseInfoDtoProfile()
        {
            CreateMap<CourseInfoDto, CoursePageViewModel>();
        }
    }
}
