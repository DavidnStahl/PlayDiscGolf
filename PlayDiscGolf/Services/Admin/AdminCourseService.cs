﻿using AutoMapper;
using PlayDiscGolf.Business.Calculations.Hole;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminCourseService : IAdminCourseService
    {       
        private readonly ICourseRepository _courseRepository;
        private readonly IHoleRepository _holeRepository;
        private readonly IMapper _mapper;
        private readonly ICreateHolesCalculation _createHolesCalcultion;

        public AdminCourseService(ICourseRepository courseRepository,IHoleRepository holeRepository, IMapper mapper,
            ICreateHolesCalculation createHolesCalcultion)
        {
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
            _mapper = mapper;
            _createHolesCalcultion = createHolesCalcultion;
        }

        public async Task<List<Hole>> GetCoursesHolesAsync(Guid id) => await _holeRepository.GetHolesByCourseIDAsync(id);

        public async Task SaveUpdatedCourseAsync(CourseFormViewModel course)
        {
            _courseRepository.EditCourse(_mapper.Map(course, await _courseRepository.GetCourseByIDAsync(course.CourseID)));

            await _courseRepository.SaveChangesAsync();
        }

        public async Task<Course> GetCourseByIDAsync(Guid id)
        {
            var course = await _courseRepository.GetCourseByIDAsync((id));

            course.Holes = await _holeRepository.GetHolesByCourseIDAsync(id);

            return course;
        }
            

        public CreateHolesViewModel ManageNumberOfHolesFromForm(CreateHolesViewModel model) => 
            _createHolesCalcultion.ConfigureHoles(model);

        public async Task<List<Course>> GetCoursesBySearchAsync(SearchViewModel model) =>
            !string.IsNullOrWhiteSpace(model.Query) && model.Type == EnumHelper.SearchType.Area.ToString() ?
            await _courseRepository.GetCoursesByAreaQueryAsync(model.Query) : await _courseRepository.GetCoursesByFullNameQueryAsync(model.Query);

    }
}
