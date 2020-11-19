using PlayDiscGolf.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.Calculations.ScoreCard
{
    public class ScoreCardCalculation : IScoreCardCalculation
    {
        public int BestRound(List<ScoreCardDto> scoreCards, string userID) => 
            (scoreCards.Select(playerCard => playerCard.PlayerCards) as IEnumerable<PlayerCardDto>).Where(p => p.UserID == userID)
            .Select(p => p.TotalScore).Distinct().Max();

        public double AverageRound(List<ScoreCardDto> scoreCards, string userID) =>
            (scoreCards.Select(playerCard => playerCard.PlayerCards) as IEnumerable<PlayerCardDto>).Where(p => p.UserID == userID)
            .Select(p => p.TotalScore).Average();
    }
}
