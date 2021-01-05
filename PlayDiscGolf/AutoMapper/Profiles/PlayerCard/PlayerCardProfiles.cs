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
            CreateMap<PlayerCardDto, PlayerCardViewModel>();
        }
    }
}
