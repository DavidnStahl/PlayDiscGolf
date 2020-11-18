using AutoMapper;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.ViewModels.Home;
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
            CreateMap<Course, CourseFormViewModel>();
            CreateMap<CourseFormViewModel, Course>();

            CreateMap<Course, SearchCourseItemViewModel>();

            CreateMap<Course, SearchResultAjaxFormViewModel>();           
            CreateMap<SearchResultAjaxFormViewModel, Course>();
        }
    }
}
