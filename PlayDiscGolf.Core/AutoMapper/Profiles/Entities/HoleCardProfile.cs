using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.Entities
{
    public class HoleCardProfile : Profile
    {
        public HoleCardProfile()
        {
            CreateMap<HoleCard, HoleCardDto>();
            CreateMap<HoleCardDto, HoleCard>();
        }
    }
}
