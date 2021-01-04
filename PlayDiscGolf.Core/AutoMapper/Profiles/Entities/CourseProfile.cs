using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.Entities
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
        }
    }
}
