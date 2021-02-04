using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf.AutoMapper.Profiles.PlayerCard
{
    public class PlayerCardProfiles : Profile
    {
        public PlayerCardProfiles()
        {
            CreateMap<PlayerCardViewModel, PlayerCardDto>();
            CreateMap<PlayerCardDto, PlayerCardViewModel>()
                .ForMember(x => x.HoleCards, source => source.MapFrom(x => x.HoleCards))
                .ForMember(x => x.ScoreCardID, source => source.MapFrom(x => x.ScoreCardID))
                .ForMember(x => x.UserID, source => source.MapFrom(x => x.UserID))
                .ForMember(x => x.UserName, source => source.MapFrom(x => x.UserName))
                .ForMember(x => x.TotalScore, source => source.MapFrom(x => x.TotalScore))
                .ForMember(x => x.PlayerCardID, source => source.MapFrom(x => x.PlayerCardID));
        }
    }
}
