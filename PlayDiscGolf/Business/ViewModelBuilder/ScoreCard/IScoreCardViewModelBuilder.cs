using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.ViewModelBuilder.ScoreCard
{
    public interface IScoreCardViewModelBuilder
    {
        public Task<ScoreCardViewModel> BuildScoreCardCreateInformation(string courseID);

        public Task<ScoreCardViewModel> BuildUpdatedScoreCardWithUpdatedPlayers(ScoreCardViewModel sessionModel, string newName);
    }
}
