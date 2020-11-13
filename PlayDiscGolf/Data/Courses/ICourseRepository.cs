using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Courses
{
    public interface ICourseRepository
    {
        public Task SaveChangesAsync();
        public Task<Course> CreateCourseAsync(Course course);

        public Course EditCourseAsync(Course course);

        public void DeleteCourseAsync(Course course);

        public Task<Course> GetCourseByIDAsync(Guid courseID);

        public Task<List<Course>> GetCoursesByLocationID(Guid locationID);
    }
}
