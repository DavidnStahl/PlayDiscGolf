using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.ViewModels.Course;

namespace PlayDiscGolf.AutoMapper.Profiles.Course
{
    public class CourseInfoProfiles : Profile
    {
        public CourseInfoProfiles()
        {
            CreateMap<CourseInfoViewModel, CourseInfoDto>();
            CreateMap<CourseInfoDto, CourseInfoViewModel>();
        }
    }
}
