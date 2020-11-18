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

        public async Task<Course> CreateCourseAsync(Course course)
        {
            var updatedCourse = await _context.Courses.AddAsync(course);
            return updatedCourse.Entity;
        }

        public void DeleteCourseAsync(Course course)
        {
            _context.Remove(course);
        }

        public Course EditCourseAsync(Course course)
        {
            return _context.Courses.Update(course).Entity;
        }

        public async Task<List<string>> GetAllCoursesCountriesAsync()
        {
            return await _context.Courses.Select(course => course.Country)
                                         .Distinct()
                                         .ToListAsync();
        }

        public async Task<Course> GetCourseByIDAsync(Guid courseID)
        {
            return await _context.Courses.Include(c => c.Holes)
                                         .FirstOrDefaultAsync(course => course.CourseID == courseID);
        }

        public async Task<List<Course>> GetCoursesByAreaQueryAsync(string query)
        {
            return await _context.Courses.Where(course => course.Area
                                         .StartsWith(query))
                                         .OrderBy(c => c.Area)
                                         .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByCountryAreaAndQueryAsync(string country, string query)
        {
            return await _context.Courses.Where(course => course.Country == country && course.Area
                                         .StartsWith(query))
                                         .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByCountryFullNameAndQueryAsync(string country, string query)
        {
            return await _context.Courses.Where(course => course.Country == country && course.FullName
                                         .StartsWith(query))
                                         .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByFullNameQueryAsync(string query)
        {
            return await _context.Courses.Where(course => course.FullName
                                         .StartsWith(query))
                                         .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
