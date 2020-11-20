
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Cards.Holes
{
    public interface IHoleCardRepository
    {
        public void UpdateHoleCard(HoleCard holeCard);

        public Task SaveChangesAsync();

    }
}
