using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Cards.Holes;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PlayDiscGolf.Data
{
    public class HoleCardRepository : IHoleCardRepository
    {
        private readonly DataBaseContext _context;

        public HoleCardRepository(DataBaseContext context)
        {
            _context = context;
        }

        public  void UpdateHoleCard(HoleCard holeCard)
        {
            _context.HoleCards.Add(holeCard);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
