using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Score
{
    public interface IScoreCardService
    {
        ScoreCardViewModel GetScoreCardCreateInformation(string courseID);
        List<PlayerCardViewModel> AddPlayerToSessionAndReturnUpdatedPlayers(string newName);

        List<PlayerCardViewModel> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer);

        ScoreCardGameOnViewModel StartGame();

        ScoreCardGameOnViewModel UpdateScore(string scoreCardID, string holeNumber, string addOrRemove, string userName);
        ScoreCardGameOnViewModel OpenScoreCard(string scoreCardID);
    }
}
