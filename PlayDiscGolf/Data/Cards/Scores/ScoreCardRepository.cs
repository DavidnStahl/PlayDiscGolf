using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Cards.Scores;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PlayDiscGolf.Data
{
    public class ScoreCardRepository : IScoreCardRepository
    {
        private readonly DataBaseContext _context;

        public ScoreCardRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<List<ScoreCard>> GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(string userID)
        {
            return await _context.ScoreCards.Include(players => players.PlayerCards)
                                            .ThenInclude(holes => holes.HoleCards)
                                            .Where(scorecards => scorecards.UserID == userID)
                                            .ToListAsync();
        }
    }
}
