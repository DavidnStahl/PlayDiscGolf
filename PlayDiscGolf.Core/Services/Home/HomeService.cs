using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.Core.Enums;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlayDiscGolf.Core.Services.Home
{
    public class HomeService : IHomeService
    {

        private readonly IUnitOfwork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeService(
            IUnitOfwork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<CourseDto> GetCourseBySearchQuery(SearchFormHomeDto model)
        {
            var courses = new List<Course>();
            if (model.Type == EnumHelper.SearchType.Area.ToString())
            {
                courses = _unitOfWork.Courses.FindBy(course => course.Country == model.Country && course.Area.StartsWith(model.Query)).ToList();
                return _mapper.Map<List<CourseDto>>(courses);
            }

            courses = _unitOfWork.Courses.FindBy(course => course.Country == model.Country && course.FullName.StartsWith(model.Query)).ToList();
            return _mapper.Map<List<CourseDto>>(courses);   
        }

        public SearchFormHomeDto ConfigureCountriesAndTypes(SearchFormHomeDto model)
        {
            model.Countries = _unitOfWork.Courses.GetAll().Select(x => new SelectListItem { Value = x.Country, Text = x.Country }).ToList();

            model.Types = new List<SelectListItem>{
                new SelectListItem{Value = EnumHelper.SearchType.Area.ToString(), Text = EnumHelper.SearchType.Area.ToString() },
                new SelectListItem{Value = EnumHelper.SearchType.Course.ToString(), Text = EnumHelper.SearchType.Course.ToString() }
            };

            return model;
        }
    }
}
