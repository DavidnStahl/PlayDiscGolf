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
            var courses = _unitOfWork.Courses.FindAllBy(course => course.Country == model.Country && course.Area.StartsWith(model.Query));

            return _mapper.Map<List<SearchResultAjaxFormDto>>(courses);
        }

        public List<SearchResultAjaxFormDto> TypeIsCourse(SearchFormHomeDto model)
        {
            var courses = _unitOfWork.Courses.FindAllBy(course => course.Country == model.Country && course.FullName.StartsWith(model.Query));

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
            return MapCourse(course);
        }

        public List<CourseDto> GetCoursesBySearch(SearchDto model)
        {
            List<Course> course;
            
            if (!string.IsNullOrWhiteSpace(model.Query) && model.Type == EnumHelper.SearchType.Area.ToString())
            {
                course = _unitOfWork.Courses.FindAllBy(x => x.Area.StartsWith(model.Query));
                course.OrderBy(x => x.Area);
                return _mapper.Map<List<CourseDto>>(course.ToList());
            }           

            course = _unitOfWork.Courses.FindAllBy(x => x.FullName.StartsWith(model.Query));
            
            return _mapper.Map<List<CourseDto>>(course.OrderBy(x => x.FullName).ToList());
        }

        public List<HoleDto> GetCoursesHoles(Guid id)
        {
            var holes = _unitOfWork.Holes.FindAllBy(x => x.CourseID == id);
            var orderedHoles = holes.OrderByDescending(x => x.HoleNumber).ToList();
            return _mapper.Map<List<HoleDto>>(orderedHoles);
        }

        public CreateHolesDto ManageNumberOfHolesFromForm(CreateHolesDto model)
        {
            return _createHolesCalcultion.ConfigureHoles(model);
        }

        public void SaveUpdatedCourse(CourseFormDto model)
        {
            var course = _mapper.Map(model, _unitOfWork.Courses.FindById(model.CourseID));
            course.Holes = _mapper.Map(model.Holes, _unitOfWork.Holes.FindAllBy(x => x.CourseID == model.CourseID));
            _unitOfWork.Courses.Edit(course);
            _unitOfWork.Complete();
        }

        private CourseFormDto MapCourse(Course course)
        {
            var model = _mapper.Map<CourseFormDto>(course);
            model.CreateHoles.CourseID = model.CourseID;
            model.CreateHoles.NumberOfHoles = model.HolesTotal;
            model.Holes = _mapper.Map<List<CourseHolesDto>>(model.Holes);

            return model;
        }
    }
}
