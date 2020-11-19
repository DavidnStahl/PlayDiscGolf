using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataBaseContext _context;
        public CourseRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task CreateCourseAsync(Course course) =>
            await _context.Courses.AddAsync(course);

        public void DeleteCourseAsync(Course course) =>
            _context.Remove(course);

        public void EditCourseAsync(Course course) =>
            _context.Courses.Update(course);

        public async Task<List<string>> GetAllCoursesCountriesAsync() => 
            await _context.Courses.Select(course => course.Country).Distinct().ToListAsync();


        public async Task<Course> GetCourseByIDAsync(Guid courseID) => 
            await _context.Courses.Include(c => c.Holes).FirstOrDefaultAsync(course => course.CourseID == courseID);
            
        public async Task<List<Course>> GetCoursesByAreaQueryAsync(string query) => 
            await _context.Courses.Where(course => course.Area.StartsWith(query)).OrderBy(c => c.Area).ToListAsync();
   
        public async Task<List<Course>> GetCoursesByCountryAreaAndQueryAsync(string country, string query) => 
            await _context.Courses.Where(course => course.Country == country && course.Area.StartsWith(query)).ToListAsync();
 

        public async Task<List<Course>> GetCoursesByCountryFullNameAndQueryAsync(string country, string query) =>
            await _context.Courses.Where(course => course.Country == country && course.FullName.StartsWith(query)).ToListAsync();
  
        public async Task<List<Course>> GetCoursesByFullNameQueryAsync(string query) => 
            await _context.Courses.Where(course => course.FullName.StartsWith(query)).ToListAsync();


        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
