using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.Entities
{
    public class PlayerCardProfile : Profile
    {
        public PlayerCardProfile()
        {
            CreateMap<PlayerCard, PlayerCardDto>();
            CreateMap<PlayerCardDto, PlayerCard>();
        }
    }
}
