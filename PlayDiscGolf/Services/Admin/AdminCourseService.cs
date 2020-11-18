
using AutoMapper;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminCourseService : IAdminCourseService
    {       
        private readonly ICourseRepository _courseRepository;
        private readonly IHoleRepository _holeRepository;
        private readonly IMapper _mapper;

        public AdminCourseService(ICourseRepository courseRepository,
                            IHoleRepository holeRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
            _mapper = mapper;
        }

        public async Task<List<Hole>> GetCoursesHoles(Guid id)
        {
            return await _holeRepository.GetHolesByCourseID(id);
        }

        public async Task SaveUpdatedCourse(CourseFormViewModel course)
        {
            var oldcourse = await _courseRepository.GetCourseByIDAsync(course.CourseID);

            oldcourse.Area = course.Area;
            oldcourse.Name = course.Name;
            oldcourse.FullName = course.FullName;                        
            oldcourse.Main = course.Main;
            oldcourse.TotalDistance = course.TotalDistance;
            oldcourse.TotalParValue = course.TotalParValue;
            oldcourse.HolesTotal = course.HolesTotal;
            oldcourse.Holes = _mapper.Map<List<Hole>>(course.Holes);

            _courseRepository.EditCourseAsync(oldcourse);
            await _courseRepository.SaveChangesAsync();
        }
        public async Task<Course> GetCourseByID(Guid id)
        {
            return await _courseRepository.GetCourseByIDAsync((id));
        }

        public async Task<List<Course>> GetCoursesByAreaQuery(string query)
        {
            return await _courseRepository.GetCoursesByAreaQueryAsync(query);
        }

        public async Task<List<Course>> GetCoursesByCourseNameQuery(string query)
        {
            return await _courseRepository.GetCoursesByFullNameQueryAsync(query);
        }

        public CreateHolesViewModel ManageNumberOfHolesFromForm(CreateHolesViewModel model)
        {
            if (model.Holes.Count < model.NumberOfHoles)
            {
                for (int i = model.Holes.Count; i < model.NumberOfHoles; i++)
                {
                    model.Holes.Add(new CourseFormViewModel.CourseHolesViewModel
                    {
                        CourseID = model.CourseID,
                        HoleNumber = i + 1,
                        HoleID = Guid.NewGuid(),
                        ParValue = 1,
                        Distance = 1
                    });
                }
            }
            else if (model.Holes.Count > model.NumberOfHoles)
            {
                var newHolesList = model.Holes;
                for (int i = model.NumberOfHoles; i < model.Holes.Count; i++)
                {
                    newHolesList.RemoveAt(i);
                }
                model.Holes = newHolesList;
            }

            return model;
        }

        
    }
}
