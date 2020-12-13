using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Claim
{
    public class ClaimScoreCardRepository : IClaimScoreCardRepository
    {
        private readonly DataBaseContext _context;

        public ClaimScoreCardRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task CreateClaimScoreCardAsync(ClaimScoreCard claimScoreCard) =>
            await _context.ClaimScoreCards.AddAsync(claimScoreCard);

        public void DeleteClaimScoreCard(ClaimScoreCard claimScoreCard) =>
            _context.ClaimScoreCards.Remove(claimScoreCard);

        public async Task<IEnumerable<ClaimScoreCard>> GetAllClaimScoreCardsByUserIDAsync(Guid userID) =>
            await _context.ClaimScoreCards.Where(claim => claim.UserID == userID).ToListAsync();


        public async Task<ClaimScoreCard> GetClaimScoreCardByIDAsync(Guid claimID) =>
            await _context.ClaimScoreCards.FirstOrDefaultAsync(Claim => Claim.ClaimID == claimID);

        
        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public void UpdateClaimScoreCard(ClaimScoreCard claimScoreCard) =>
            _context.Update(claimScoreCard);
    }
}
