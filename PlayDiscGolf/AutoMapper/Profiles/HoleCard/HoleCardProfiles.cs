using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.AutoMapper.Profiles.HoleCard
{
    public class HoleCardProfiles : Profile
    {
        public HoleCardProfiles()
        {
            CreateMap<HoleCardViewModel, HoleCardDto>();
            CreateMap<HoleCardDto, HoleCardViewModel>();
        }
    }
}
