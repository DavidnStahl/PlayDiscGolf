using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class CourseRepository : EntityRepository<Course>, ICourseRepository
    {
        private readonly DataBaseContext _context;

        public CourseRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public Course GetCourseByIDAndIncludeHoles(Guid id)
        {
            return _context.Courses
                .Include(x => x.Holes)
                .SingleOrDefault(x => x.CourseID == id);
        }
    }
}
