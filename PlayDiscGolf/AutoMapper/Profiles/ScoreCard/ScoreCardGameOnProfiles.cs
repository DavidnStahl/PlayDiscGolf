using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf.AutoMapper.Profiles.ScoreCard
{
    public class ScoreCardGameOnProfiles : Profile
    {
        public ScoreCardGameOnProfiles()
        {
            CreateMap<ScoreCardGameOnViewModel, ScoreCardGameOnDto>();
            CreateMap<ScoreCardGameOnDto, ScoreCardGameOnViewModel>();
        }
    }
}
