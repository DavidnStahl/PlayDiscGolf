using AutoMapper;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Profiles
{
    public class PlayerCardProfile : Profile
    {
        public PlayerCardProfile()
        {
            CreateMap<PlayerCardViewModel, PlayerCard>();

        }
    }
}
