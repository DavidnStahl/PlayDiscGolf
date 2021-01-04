using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Business.Calculations.ScoreCard
{
    public interface IScoreCardCalculation
    {
        public int BestRound(List<ScoreCardDto> scoreCards, string userID);

        public int AverageRound(List<ScoreCardDto> scoreCards, string userID);
    }
}
