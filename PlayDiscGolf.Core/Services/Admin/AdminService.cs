using AutoMapper;
using PlayDiscGolf.Core.Business.Calculations.Hole;
using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Core.Dtos.Entities;
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
            var course = _unitOfWork.Courses.FindById(model.CourseID);
            var editedCourse = _mapper.Map(model, course);
            _unitOfWork.Courses.Edit(editedCourse);
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
