using AutoMapper;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.Core.Enums;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

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

        public List<SearchResultAjaxFormDto> TypeIsArea(SearchFormHomeDto model)
        {
            var courses = _unitOfWork.Courses.FindAllBy(course => course.Country == model.Country && course.Area.StartsWith(model.Query) && course.HolesTotal > 0);

            return _mapper.Map<List<SearchResultAjaxFormDto>>(courses);
        }

        public List<SearchResultAjaxFormDto> TypeIsCourse(SearchFormHomeDto model)
        {
            var courses = _unitOfWork.Courses.FindAllBy(course => course.Country == model.Country && course.FullName.StartsWith(model.Query) && course.HolesTotal > 0);

            return _mapper.Map<List<SearchResultAjaxFormDto>>(courses);
        }

        public List<SearchResultAjaxFormDto> GetCourseBySearchQuery(SearchFormHomeDto model)
        {
            if (model.Type == EnumHelper.SearchType.Area.ToString())
                return TypeIsArea(model);

            return TypeIsCourse(model);
        }

        public SearchFormHomeDto ConfigureCountriesAndTypes(SearchFormHomeDto model)
        {
            model.Countries = _unitOfWork.Courses.GetAll().Select(x => x.Country).Distinct().ToList();
            model.Types = new List<string> { EnumHelper.SearchType.Area.ToString(), EnumHelper.SearchType.Course.ToString() };
            return model;
        }
    }
}
