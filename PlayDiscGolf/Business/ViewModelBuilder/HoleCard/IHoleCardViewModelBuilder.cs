using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.ViewModelBuilder.HoleCard
{
    public interface IHoleCardViewModelBuilder
    {
        public Task<List<HoleCardViewModel>> BuildHoleCardsForCourseAsync(Guid courseID, Guid playerCardID);
    }
}
