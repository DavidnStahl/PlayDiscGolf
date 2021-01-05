using AutoMapper;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.AdminCourse
{
    public class CourseFormProfiles : Profile
    {
        public CourseFormProfiles()
        {
            CreateMap<CourseFormViewModel, CourseFormDto>();
            CreateMap<CourseFormDto, CourseFormViewModel>();
        }
    }
}
