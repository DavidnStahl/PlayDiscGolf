using AutoMapper;
using PlayDiscGolf.Core.Business.Calculations.Hole;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Core.Dtos.Home;
using PlayDiscGolf.Core.Dtos.PostModels;
using PlayDiscGolf.Core.Enums;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayDiscGolf.Core.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICreateHolesCalculation _createHolesCalcultion;

        public AdminService(
            IUnitOfwork unitOfWork,
            IMapper mapper,
            ICreateHolesCalculation createHolesCalcultion)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createHolesCalcultion = createHolesCalcultion;
        }

        public List<SearchResultAjaxFormDto> TypeIsArea(SearchFormHomeDto model)
        {
            var courses = _unitOfWork.Courses.FindBy(course => course.Country == model.Country && course.Area.StartsWith(model.Query)).ToList();

            return _mapper.Map<List<SearchResultAjaxFormDto>>(courses);
        }

        public List<SearchResultAjaxFormDto> TypeIsCourse(SearchFormHomeDto model)
        {
            var courses = _unitOfWork.Courses.FindBy(course => course.Country == model.Country && course.FullName.StartsWith(model.Query)).ToList();

            return _mapper.Map<List<SearchResultAjaxFormDto>>(courses);
        }

        public List<SearchResultAjaxFormDto> GetAllCourseBySearchQuery(SearchFormHomeDto model)
        {
            if (model.Type == EnumHelper.SearchType.Area.ToString())
                return TypeIsArea(model);

            return TypeIsCourse(model);
        }

        public CourseFormDto GetCourseByID(Guid id)
        {
            var course = _unitOfWork.Courses.GetCourseByIDAndIncludeHoles(id);
            var x = MapCourse(course);
            return x;
        }

        public List<CourseDto> GetCoursesBySearch(SearchDto model)
        {
            List<Course> course;
            
            if (!string.IsNullOrWhiteSpace(model.Query) && model.Type == EnumHelper.SearchType.Area.ToString())
            {
                course = _unitOfWork.Courses.FindBy(x => x.Area.StartsWith(model.Query)).OrderBy(x => x.Area).ToList();
                return _mapper.Map<List<CourseDto>>(course);
            }           

            course = _unitOfWork.Courses.FindBy(x => x.FullName.StartsWith(model.Query)).OrderBy(x => x.FullName).ToList();
            return _mapper.Map<List<CourseDto>>(course);
        }

        public List<HoleDto> GetCoursesHoles(Guid id)
        {
            var holes = _unitOfWork.Holes.FindBy(x => x.CourseID == id);
            return _mapper.Map<List<HoleDto>>(holes);
        }

        public CreateHolesDto ManageNumberOfHolesFromForm(CreateHolesDto model)
        {
            return _createHolesCalcultion.ConfigureHoles(model);
        }

        public void SaveUpdatedCourse(CourseFormDto model)
        {
            var course = _mapper.Map(model, _unitOfWork.Courses.FindById(model.CourseID));
            course.Holes = _mapper.Map(model.Holes, _unitOfWork.Holes.FindBy(x => x.CourseID == model.CourseID));
            _unitOfWork.Courses.Edit(course);
            _unitOfWork.Complete();
        }

        private CourseFormDto MapCourse(Course course)
        {
            var x = _mapper.Map<CourseFormDto>(course);
            x.CreateHoles.CourseID = x.CourseID;
            x.CreateHoles.NumberOfHoles = x.HolesTotal;
            x.Holes = _mapper.Map<List<CourseHolesDto>>(x.Holes);

            return x;
        }
    }
}
