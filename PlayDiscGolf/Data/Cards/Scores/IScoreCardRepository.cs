using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Cards.Scores
{
    public interface IScoreCardRepository
    {
        public Task<List<ScoreCard>> GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(string userID);

        public Task SaveChangesAsync();

        public Task CreateScoreCardIncludePlayerCardAsync(ScoreCard scoreCard);
    }
}
