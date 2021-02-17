using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class ScoreCardRepository : EntityRepository<ScoreCard>, IScoreCardRepository
    {
        private readonly DataBaseContext _context;

        public ScoreCardRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ScoreCard> GetAllScoreCardAndIncludePlayerCardAndHoleCardBy(Expression<Func<ScoreCard, bool>> predicate)
        {
            return _context
                .Set<ScoreCard>()
                .Include(x => x.PlayerCards)
                .ThenInclude(x => x.HoleCards)
                .Where(predicate).ToList();
                
        }

        public ScoreCard GetSingleScoreCardAndIncludePlayerCardAndHoleCardBy(Expression<Func<ScoreCard, bool>> predicate)
        {
            return _context
                .Set<ScoreCard>()
                .Include(x => x.PlayerCards)                
                .ThenInclude(x => x.HoleCards)
                .SingleOrDefault(predicate);

        }

        public IEnumerable<ScoreCard> GetScoreCardAndIncludeWhenNotTheOwner(string userID, Guid courseID)
        {
            return _context.Set<ScoreCard>()
                .Include(x => x.PlayerCards)
                .ThenInclude(x => x.HoleCards)
                .Where(x => x.CourseID == courseID && x.UserID == userID)                
                .SelectMany(x => x.PlayerCards)
                .Where(x => x.UserID == userID)
                .Select(x => x.Scorecard)
                .OrderByDescending(x => x.StartDate)
                .Distinct()
                .ToList();
        }
    }
}
