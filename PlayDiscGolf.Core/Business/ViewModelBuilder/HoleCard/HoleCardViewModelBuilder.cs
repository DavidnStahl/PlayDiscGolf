using PlayDiscGolf.Data;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.ViewModelBuilder.HoleCard
{
    public class HoleCardViewModelBuilder : IHoleCardViewModelBuilder
    {
        private readonly IEntityRepository<Hole> _holeRepository;

        public HoleCardViewModelBuilder(IEntityRepository<Hole> holeRepository)
        {
            _holeRepository = holeRepository;
        }
        public List<HoleCardViewModel> BuildHoleCardsForCourse(Guid courseID, Guid playerCardID)
        {
            var holeCardViewModelList = new List<HoleCardViewModel>();

            var holes = _holeRepository.FindBy(x => x.CourseID == courseID);

            for (int i = 0; i < holes.Count; i++)
                holeCardViewModelList.Add(new HoleCardViewModel
                {
                    HoleCardID = Guid.NewGuid(),
                    HoleNumber = i + 1,
                    PlayerCardID = playerCardID,
                    Score = 0
                });

            return holeCardViewModelList;
        }
    }
}
