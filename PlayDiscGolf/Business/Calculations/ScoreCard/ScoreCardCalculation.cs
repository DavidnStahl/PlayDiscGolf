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
            (scoreCards as IEnumerable<ScoreCardDto>).SelectMany(p => p.PlayerCards).Where(p => p.UserID == userID)
            .Select(p => p.TotalScore).Min();

        public int AverageRound(List<ScoreCardDto> scoreCards, string userID) =>
            Convert.ToInt32((scoreCards as IEnumerable<ScoreCardDto>).SelectMany(p => p.PlayerCards).Where(p => p.UserID == userID)
            .Select(p => p.TotalScore).Average());
    }
}
