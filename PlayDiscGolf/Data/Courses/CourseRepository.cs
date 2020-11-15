using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Models.DataModels;
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
            await _context.Courses.AddAsync(course);
            return course;
        }

        public void DeleteCourseAsync(Course course)
        {
            _context.Remove(course);
        }

        public Course EditCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            return course;
        }

        public async Task<Course> GetCourseByIDAsync(Guid courseID)
        {
            return await _context.Courses.FirstOrDefaultAsync(course => course.CourseID == courseID);
        }

        public async Task<List<Course>> GetCoursesByAreaQueryAsync(string query)
        {
            return await _context.Courses.Where(course => course.Area.StartsWith(query)).OrderBy(c => c.Area).ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByFullNameQueryAsync(string query)
        {
            return await _context.Courses.Where(course => course.FullName.StartsWith(query)).OrderBy(c => c.FullName).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
