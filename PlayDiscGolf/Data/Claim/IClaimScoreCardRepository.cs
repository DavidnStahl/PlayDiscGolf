using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Claim
{
    public interface IClaimScoreCardRepository
    {
        Task CreateClaimScoreCardAsync(ClaimScoreCard claimScoreCard);

        Task<IEnumerable<ClaimScoreCard>> GetAllClaimScoreCardsByUserIDAsync(Guid userID);

        Task<ClaimScoreCard> GetClaimScoreCardByIDAsync(Guid claimID);

        void UpdateClaimScoreCard(ClaimScoreCard claim);

        void DeleteClaimScoreCard(ClaimScoreCard claim);

        Task SaveChangesAsync();
    }
}
