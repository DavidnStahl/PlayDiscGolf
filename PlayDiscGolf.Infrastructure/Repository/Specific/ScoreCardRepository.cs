using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class ScoreCardRepository : EntityRepository<ScoreCard>, IScoreCardRepository
    {
        private readonly DataBaseContext _context;

        public ScoreCardRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public List<ScoreCard> GetScoreCardsByUserNameAndCourseIDAndIncludePlayerCardAndHoleCard(string userID, Guid courseID)
        {
            return _context.ScoreCards
                .Include(x => x.PlayerCards)
                .ThenInclude(x => x.HoleCards)
                .Where(x => x.UserID == userID && x.CourseID == courseID)
                .OrderByDescending(x => x.StartDate)
                .ToList();
        }
    }
}
