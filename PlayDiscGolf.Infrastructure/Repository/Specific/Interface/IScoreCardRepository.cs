using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;

namespace PlayDiscGolf.Infrastructure.Repository.Specific.Interface
{
    public interface IScoreCardRepository : IEntityRepository<ScoreCard>
    {
        List<ScoreCard> GetScoreCardsByUserNameAndCourseIDAndIncludePlayerCardAndHoleCard(string userID, Guid courseID);
    }
}
