using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.Entities
{
    public class ScoreCardProfile : Profile
    {
        public ScoreCardProfile()
        {
            CreateMap<ScoreCard, ScoreCardDto>();
            CreateMap<ScoreCardDto, ScoreCard>();
        }
    }
}
