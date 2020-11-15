using AutoMapper;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            //Source -> Target
            CreateMap<Course, CourseFormViewModel>();
            CreateMap<CourseFormViewModel, Course>();
            CreateMap<Course, SearchCourseItemViewModel>();
        }
    }
}
