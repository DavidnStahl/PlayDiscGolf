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

        public async Task<List<ScoreCard>> GetAllScoreCardsAndIncludePlayerCardsByUserNameAsync(string userName)
        {
            return await _context.ScoreCards.Include(s => s.PlayerCards).Where(scoreCard => scoreCard.UserName == userName).ToListAsync();
        }

        public async Task<List<ScoreCard>> GetScoreCardIncludePlayerCardIncludeHoleCardByIDAsync(string userID, Guid courseID) =>
            await _context.ScoreCards
            .Include(players => players.PlayerCards)
            .ThenInclude(holes => holes.HoleCards)
            .Where(scorecards => scorecards.UserID == userID && scorecards.CourseID == courseID)
            .OrderByDescending(scorecard => scorecard.StartDate)
            .ToListAsync();

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public void UpdateScoreCard(ScoreCard scoreCard)
        {
            _context.ScoreCards.Update(scoreCard);
        }
    }
}
