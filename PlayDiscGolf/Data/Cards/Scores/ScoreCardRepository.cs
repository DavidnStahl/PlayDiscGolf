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

        public async Task CreateScoreCardIncludePlayerCardAsync(ScoreCard scoreCard) =>
            await _context.ScoreCards.AddAsync(scoreCard);

        public async Task<List<ScoreCard>> GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(string userID) =>
            await _context.ScoreCards.Include(players => players.PlayerCards).ThenInclude(holes => holes.HoleCards)
            .Where(scorecards => scorecards.UserID == userID).ToListAsync();

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
