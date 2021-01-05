using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlayDiscGolf.Infrastructure.Repository.Specific.Interface
{
    public interface IScoreCardRepository : IEntityRepository<ScoreCard>
    {
        ScoreCard GetScoreCardAndIncludePlayerCardAndHoleCard(Expression<Func<ScoreCard, bool>> predicate);
    }
}
