using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Linq;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class HoleRepository : EntityRepository<Hole>, IHoleRepository
    {
        private readonly DataBaseContext _context;

        public HoleRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public Hole GetCourseHole(Guid courseID, int holeNumber)
        {
            return _context.Holes.SingleOrDefault(x => x.CourseID == courseID && x.HoleNumber == holeNumber);
        }
    }
}
