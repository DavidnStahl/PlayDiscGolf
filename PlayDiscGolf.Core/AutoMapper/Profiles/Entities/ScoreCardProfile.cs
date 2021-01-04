using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Dtos.ScoreCard;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

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
