using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.ScoreCard
{
    public interface IScoreCardService
    {
        public Task<ScoreCardViewModel> GetScoreCardCreateInformation(string courseID);
        public Task<List<PlayerCardViewModel>> AddPlayerToSessionAndReturnUpdatedPlayers(string newName);

        public List<PlayerCardViewModel> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer);

        public Task<ScoreCardGameOnViewModel> StartScoreCard();

        public Task<ScoreCardGameOnViewModel> UpdateScoreCard(string scoreCardID, string holeNumber, string addOrRemove, string userName);
    }
}
