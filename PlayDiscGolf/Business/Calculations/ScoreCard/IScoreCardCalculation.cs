using PlayDiscGolf.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.Calculations.ScoreCard
{
    public interface IScoreCardCalculation
    {
        public int BestRound(List<ScoreCardDto> scoreCards, string userID);

        public double AverageRound(List<ScoreCardDto> scoreCards, string userID);
    }
}
