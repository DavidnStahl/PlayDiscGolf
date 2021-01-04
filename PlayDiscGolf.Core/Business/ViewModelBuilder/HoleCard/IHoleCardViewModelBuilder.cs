using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.ViewModelBuilder.HoleCard
{
    public interface IHoleCardViewModelBuilder
    {
        List<HoleCardViewModel> BuildHoleCardsForCourse(Guid courseID, Guid playerCardID);
    }
}
