using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.ViewModels.User;

namespace PlayDiscGolf.AutoMapper.Profiles.User
{
    public class FriendProfiles : Profile
    {
        public FriendProfiles()
        {
            CreateMap<FriendDto, FriendViewModel>()
                .ForMember(x => x.FriendID, source => source.MapFrom(x => x.FriendID.ToString()))
                .ForMember(x => x.FriendRequestAccepted, source => source.MapFrom(x => x.FriendRequestAccepted))
                .ForMember(x => x.UserID, source => source.MapFrom(x => x.UserID.ToString()))
                .ForMember(x => x.UserName, source => source.MapFrom(x => x.UserName))
                .ForMember(x => x.FriendUserID, source => source.MapFrom(x => x.FriendUserID.ToString()));

            CreateMap<FriendViewModel ,FriendDto>()
                .ForMember(x => x.FriendID, source => source.MapFrom(x => Guid.Parse(x.FriendID)))
                .ForMember(x => x.FriendRequestAccepted, source => source.MapFrom(x => x.FriendRequestAccepted))
                .ForMember(x => x.UserID, source => source.MapFrom(x => Guid.Parse(x.UserID)))
                .ForMember(x => x.UserName, source => source.MapFrom(x => x.UserName))
                .ForMember(x => x.FriendUserID, source => source.MapFrom(x => Guid.Parse(x.FriendUserID)));
        }
    }
}
