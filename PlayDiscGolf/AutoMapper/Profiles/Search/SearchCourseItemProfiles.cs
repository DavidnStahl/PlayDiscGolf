using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Models.ViewModels;

namespace PlayDiscGolf.AutoMapper.Profiles.AdminCourse
{
    public class SearchCourseItemProfiles : Profile
    {
        public SearchCourseItemProfiles()
        {
            CreateMap<SearchCourseItemViewModel, SearchCourseItemDto>();
            CreateMap<SearchCourseItemDto, SearchCourseItemViewModel>();
        }
    }
}
