using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Cards.Players
{
    public interface IPlayerCardRepository
    {
        public Task<List<PlayerCard>> GetPlayerCardsByScoreCardID(Guid scorecardID);

        public void UpdatePlayerCard(PlayerCard playerCard);

        public Task SaveChangesAsync();
    }
}
