
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminService : IAdminService
    {       
        private readonly ICourseRepository _courseRepository;
        private readonly IHoleRepository _holeRepository;

        public AdminService(ICourseRepository courseRepository,
                            IHoleRepository holeRepository)
        {
            _courseRepository = courseRepository;
            _holeRepository = holeRepository;
        }

        public async Task<List<Hole>> GetCoursesHoles(Guid id)
        {
            return await _holeRepository.GetHolesByCourseID(id);
        }

        public async Task SaveUpdatedCourse(Course course)
        {

            var oldcourse = await _courseRepository.GetCourseByIDAsync(course.CourseID);

            oldcourse.Area = course.Area;
            oldcourse.Name = course.Name;
            oldcourse.FullName = course.FullName;                        
            oldcourse.Main = course.Main;
            oldcourse.TotalDistance = course.TotalDistance;
            oldcourse.TotalParValue = course.TotalParValue;
            oldcourse.HolesTotal = course.HolesTotal;
            oldcourse.Holes = course.Holes;
            _courseRepository.EditCourseAsync(oldcourse);
            await _courseRepository.SaveChangesAsync();


            /*if (oldcourse.Holes.Count == course.HolesTotal)
            {
                oldcourse.HolesTotal = course.HolesTotal;
                oldcourse.Holes = course.Holes;
                _courseRepository.EditCourseAsync(oldcourse);
                await _courseRepository.SaveChangesAsync();
            }
            else if(oldcourse.Holes.Equals(course.Holes) && oldcourse.HolesTotal == course.HolesTotal)
            {
                oldcourse.Holes = course.Holes;
                _courseRepository.EditCourseAsync(oldcourse);
                await _courseRepository.SaveChangesAsync();
            }
            else if(oldcourse.HolesTotal < course.HolesTotal || oldcourse.HolesTotal > course.HolesTotal)
            {

                /*
                    if(oldcourse.HolesTotal < course.HolesTotal)
                    {
                    oldcourse.Holes = course.Holes;

                    for (int i = 0; i < course.HolesTotal; i++)
                        {
                            var newHole = new Hole
                            {
                                CourseID = oldcourse.CourseID,
                                HoleID = Guid.NewGuid(),
                                HoleNumber = i + 1
                            };

                            oldcourse.Holes.Add(newHole);

                        }
                    }
                    else
                    {
                       oldcourse.Holes = course.Holes;
                    }

                oldcourse.Holes = course.Holes;
                oldcourse.HolesTotal = course.HolesTotal;
                _courseRepository.EditCourseAsync(oldcourse);
                await _courseRepository.SaveChangesAsync();
            }*/
        }

        public async Task SaveNewHoles(List<Hole> holes)
        {

            await _holeRepository.CreateHolesAsync(holes);
            await _holeRepository.SaveChangesAsync();
        }

        public async Task<Course> GetCourseByID(Guid id)
        {
            return await _courseRepository.GetCourseByIDAsync((id));
        }

        public async Task<List<Course>> GetCoursesByLocationQuery(string query)
        {
            return await _courseRepository.GetCoursesByAreaQueryAsync(query);
        }

        public async Task<List<Course>> GetCoursesByCourseNameQuery(string query)
        {
            return await _courseRepository.GetCoursesByFullNameQueryAsync(query);
        }


        public async Task DeleteHoles(List<Hole> holes)
        {
            _holeRepository.DeleteHoles(holes);
            await _holeRepository.SaveChangesAsync();
        }

        public async Task CheckAndRemoveOrAddHole(Course course)
        {
            var oldcourse = await _courseRepository.GetCourseByIDAsync(course.CourseID);

            if (oldcourse.HolesTotal > course.HolesTotal)
            {
                var courseHoles = await _holeRepository.GetHolesByCourseID(oldcourse.CourseID);

                for (int i = course.HolesTotal; i < oldcourse.HolesTotal; i++)
                {

                    _holeRepository.DeleteHoleAsync(courseHoles[i + 1]);
                }
            }
            else if (oldcourse.HolesTotal < course.HolesTotal)
            {

                for (int i = oldcourse.HolesTotal; i < course.HolesTotal; i++)
                {
                    var hole = new Hole
                    {
                        CourseID = course.CourseID,
                        HoleID = Guid.NewGuid(),
                        HoleNumber = i + 1
                    };
                    await _holeRepository.CreateHoleAsync(hole);
                }
            }
        }
    }
}
