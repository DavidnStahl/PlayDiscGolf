using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.Course
{
    public class CourseProfiles : Profile
    {
        public CourseProfiles()
        {
            CreateMap<CourseViewModel, CourseDto>();
            CreateMap<CourseDto, CourseViewModel>();
        }
    }
}
