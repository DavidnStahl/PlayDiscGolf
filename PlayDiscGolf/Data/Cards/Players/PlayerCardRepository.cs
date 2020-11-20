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

        public Task<List<PlayerCard>> GetPlayerCardsByScoreCardID(Guid scorecardID)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerCard(PlayerCard playerCard)
        {
            throw new NotImplementedException();
        }
    }
}
