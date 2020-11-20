using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Courses
{
    public interface ICourseRepository
    {
        public Task SaveChangesAsync();
        public Task CreateCourseAsync(Course course);

        public void EditCourse(Course course);

        public Task<List<Course>> GetCoursesByAreaQueryAsync(string query);

        public Task<List<Course>> GetCoursesByFullNameQueryAsync(string query);
        
        public void DeleteCourse(Course course);

        public Task<Course> GetCourseByIDAsync(Guid courseID);

        public Task<List<string>> GetAllCoursesCountriesAsync();

        public Task<List<Course>> GetCoursesByCountryAreaAndQueryAsync(string country, string query);

        public Task<List<Course>> GetCoursesByCountryFullNameAndQueryAsync(string country,string query);
    }
}
