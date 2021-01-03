using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Business.Calculations.Hole;
using PlayDiscGolf.Data;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using PlayDiscGolf.Models.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminCourseService : IAdminCourseService
    {       
        private readonly IEntityRepository<Course> _courseRepository;
        private readonly IEntityRepository<Hole> _holeRepository;
        private readonly IMapper _mapper;
        private readonly ICreateHolesCalculation _createHolesCalcultion;

        public AdminCourseService(IEntityRepository<Course> courseRepository, IEntityRepository<Hole> holeRepository, IMapper mapper,
            ICreateHolesCalculation createHolesCalcultion)
        {
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
            _mapper = mapper;
            _createHolesCalcultion = createHolesCalcultion;
        }

        public List<Hole> GetCoursesHoles(Guid id)
        {
            return _holeRepository.FindBy(x => x.CourseID == id);
        }

        public void SaveUpdatedCourse(CourseFormViewModel model)
        {
            var course = _courseRepository.FindById(model.CourseID);
            var editedCourse = _mapper.Map(model, course);
            _courseRepository.Edit(editedCourse);
            _courseRepository.Save();
        }

        public Course GetCourseByID(Guid id)
        {
            return _courseRepository
                .GetAll()
                .Include(x => x.Holes)
                .SingleOrDefault(x => x.CourseID == id);
        }


        public CreateHolesViewModel ManageNumberOfHolesFromForm(CreateHolesViewModel model)
        {
            return _createHolesCalcultion.ConfigureHoles(model);
        }

        public List<Course> GetCoursesBySearch(SearchViewModel model)
        {
            if(!string.IsNullOrWhiteSpace(model.Query) && model.Type == EnumHelper.SearchType.Area.ToString()) 
                return _courseRepository
                    .GetAll()
                    .Where(x => x.Area.StartsWith(model.Query))
                    .OrderBy(c => c.Area)
                    .ToList();

            return _courseRepository
                .GetAll()
                .Where(x => x.FullName.StartsWith(model.Query))
                .OrderBy(x => x.FullName)
                .ToList();
        }


    }
}
