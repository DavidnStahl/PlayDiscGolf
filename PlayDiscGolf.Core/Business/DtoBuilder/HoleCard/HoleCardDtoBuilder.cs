using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayDiscGolf.Core.Business.DtoBuilder.HoleCard
{
    public class HoleCardDtoBuilder : IHoleCardDtoBuilder
    {
        private readonly IUnitOfwork _unitOfwork;

        public HoleCardDtoBuilder(IUnitOfwork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }
        public List<HoleCardDto> BuildHoleCardsForCourse(Guid courseID, Guid playerCardID)
        {
            var holeCardDtos = new List<HoleCardDto>();

            var holes = _unitOfwork.Holes.FindAllBy(x => x.CourseID == courseID);

            for (int i = 0; i < holes.Count; i++)
                holeCardDtos.Add(new HoleCardDto
                {
                    HoleCardID = Guid.NewGuid(),
                    HoleNumber = i + 1,
                    PlayerCardID = playerCardID,
                    Score = 0
                });

            return holeCardDtos;
        }
    }
}
