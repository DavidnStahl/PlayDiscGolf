using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Cards.Players;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PlayDiscGolf.Data
{
    public class PlayerCardRepository : IPlayerCardRepository
    {
        private readonly DataBaseContext _context;

        public PlayerCardRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<List<PlayerCard>> GetPlayerCardsByScoreCardID(Guid scorecardID)
        {
            return await _context.PlayerCards.Where(playerCard => playerCard.ScoreCardID == scorecardID).ToListAsync();
        }
    }
}
