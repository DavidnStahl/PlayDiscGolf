using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.ViewModels.ScoreCard;

namespace PlayDiscGolf.AutoMapper.Profiles.ScoreCard
{
    public class ScoreCardProfiles : Profile
    {
        public ScoreCardProfiles()
        {
            CreateMap<ScoreCardViewModel, ScoreCardDto>();
            CreateMap<ScoreCardDto, ScoreCardViewModel>()
                .ForMember(x => x.CourseID, source => source.MapFrom(x => x.CourseID))
                .ForMember(x => x.UserName, source => source.MapFrom(x => x.UserName))
                .ForMember(x => x.ScoreCardID, source => source.MapFrom(x => x.ScoreCardID))
                .ForMember(x => x.StartDate, source => source.MapFrom(x => x.StartDate))
                .ForMember(x => x.EndDate, source => source.MapFrom(x => x.EndDate))
                .ForMember(x => x.UserID, source => source.MapFrom(x => x.UserID))
                .ForMember(x => x.PlayerCards, source => source.MapFrom(x => x.PlayerCards))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
