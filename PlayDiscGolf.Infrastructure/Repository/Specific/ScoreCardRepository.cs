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

        public ScoreCard GetScoreCardAndIncludePlayerCardAndHoleCard(Expression<Func<ScoreCard, bool>> predicate)
        {
            return _context
                .Set<ScoreCard>()
                .Include(x => x.PlayerCards)
                .ThenInclude(x => x.HoleCards)
                .SingleOrDefault(predicate);
                
        }
    }
}
