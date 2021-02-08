using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Core.AutoMapper.Profiles.Entities
{
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<Friend, FriendDto>();
            CreateMap<FriendDto, Friend>();
        }
    }
}
