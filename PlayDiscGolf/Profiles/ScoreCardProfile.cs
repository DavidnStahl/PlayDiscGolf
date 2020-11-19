using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf.Profiles
{
    public class ScoreCardProfile : Profile
    {
        public ScoreCardProfile()
        {
            CreateMap<ScoreCardDto, ScoreCard>();
            CreateMap<ScoreCard, ScoreCardDto>();
            CreateMap<ScoreCardViewModel, ScoreCard>();
            CreateMap<ScoreCard, ScoreCardViewModel>();
        }
    }
}
