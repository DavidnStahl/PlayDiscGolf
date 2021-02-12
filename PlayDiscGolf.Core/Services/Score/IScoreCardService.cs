using PlayDiscGolf.Core.Dtos.Cards;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.Score
{
    public interface IScoreCardService
    {
        ScoreCardDto GetScoreCardCreateInformation(string courseID);
        Task<List<PlayerCardDto>> AddPlayerToSessionAndReturnUpdatedPlayersAsync(string newName);

        List<PlayerCardDto> RemovePlayerFromSessionAndReturnUpdatedPlayers(string removePlayer);

        ScoreCardGameOnDto StartGame();

        ScoreCardGameOnDto UpdateScore(string scoreCardID, string holeNumber, string addOrRemove, string userName);
        ScoreCardGameOnDto OpenScoreCard(string scoreCardID);

        string GetCourseName(Guid courseID);
    }
}
