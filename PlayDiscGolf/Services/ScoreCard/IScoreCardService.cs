using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.ScoreCard
{
    public interface IScoreCardService
    {
        public Task<ScoreCardViewModel> GetScoreCardCreateInformationAsync(string courseID);
        public Task<List<PlayerCardViewModel>> AddPlayerToSessionAndReturnUpdatedPlayersAsync(string newName);

        public List<PlayerCardViewModel> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer);

        public Task<ScoreCardGameOnViewModel> StartScoreCardAsync();

        public Task<ScoreCardGameOnViewModel> ModifyScoreCardAsync(string scoreCardID, string holeNumber, string addOrRemove, string userName, Guid courseID);
        public Task<ScoreCardGameOnViewModel> OpenScoreCardAsync(string scoreCardID, Guid courseID);
    }
}
