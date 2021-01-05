using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.ViewModels.Course;

namespace PlayDiscGolf.AutoMapper.Profiles.Course
{
    public class CourseReviewProfiles : Profile
    {
        public CourseReviewProfiles()
        {
            CreateMap<CourseReviewViewModel, CourseReviewDto>();
            CreateMap<CourseReviewDto, CourseReviewViewModel>();
        }
    }
}
