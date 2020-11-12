using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class CourseService
    {
        private readonly DataBaseContext _context;
        public CourseService(DataBaseContext context)
        {
            _context = context;
        }

        public void SaveCourseToLocation(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();            
        }

        public void SaveHolesToCourse(List<Hole> holes)
        {
            _context.AddRange(holes);
            _context.SaveChanges();
        }

        public List<Course> GetCoursesByLocationID(int LocationID)
        {
            return _context.Courses.Where(course => course.LocationID == LocationID).ToList();
        }

        public Course GetCourseByCourseID(int CourseID)
        {
            return _context.Courses.FirstOrDefault(course => course.CourseID == CourseID);
        }

        public List<Hole> GetHolesByCourseID(int CourseID)
        {
            return _context.Holes.Where(holes => holes.CourseID == CourseID).ToList();
        }

        public Hole GetHoleByHoleNumber(int HoleNumber)
        {
            return _context.Holes.FirstOrDefault(hole => hole.HoleNumber == HoleNumber);
        }
    }
}
