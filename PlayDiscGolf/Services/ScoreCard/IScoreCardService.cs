using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.ScoreCard
{
    public interface IScoreCardService
    {
        public ScoreCardViewModel GetScoreCardCreateInformation(string courseID);
        public List<PlayerCardViewModel> AddPlayerToSessionAndReturnUpdatedPlayers(string newName);

        public List<PlayerCardViewModel> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer);

        public Task<ScoreCardGameOnViewModel> StartScoreCard();

        public Task<ScoreCardGameOnViewModel> SaveScoreCardTurn(HoleCardViewModel model);
        public Task<ScoreCardGameOnViewModel> ChangeHole(string activatedNextNumber, string courseID, string scorecardID);

        public Task EndScoreCard();

    }
}
