using PlayDiscGolf.Core.Dtos.Cards;
using System;
using System.Collections.Generic;

namespace PlayDiscGolf.Core.Business.DtoBuilder.HoleCard
{
    public interface IHoleCardDtoBuilder
    {
        List<HoleCardDto> BuildHoleCardsForCourse(Guid courseID, Guid playerCardID);
    }
}
