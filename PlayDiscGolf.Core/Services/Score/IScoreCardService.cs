using PlayDiscGolf.Core.Dtos.Cards;
using System.Collections.Generic;

namespace PlayDiscGolf.Core.Services.Score
{
    public interface IScoreCardService
    {
        ScoreCardDto GetScoreCardCreateInformation(string courseID);
        List<PlayerCardDto> AddPlayerToSessionAndReturnUpdatedPlayers(string newName);

        List<PlayerCardDto> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer);

        ScoreCardGameOnDto StartGame();

        ScoreCardGameOnDto UpdateScore(string scoreCardID, string holeNumber, string addOrRemove, string userName);
        ScoreCardGameOnDto OpenScoreCard(string scoreCardID);
    }
}
