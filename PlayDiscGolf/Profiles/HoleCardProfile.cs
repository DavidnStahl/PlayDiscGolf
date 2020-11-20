using AutoMapper;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;


namespace PlayDiscGolf.Profiles
{
    public class HoleCardProfile : Profile
    {
        public HoleCardProfile()
        {
            CreateMap<HoleCardViewModel, HoleCard>();
            CreateMap<HoleCard, HoleCardViewModel>();

        }
    }
}
