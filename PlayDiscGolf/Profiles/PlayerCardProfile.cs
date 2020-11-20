using AutoMapper;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;


namespace PlayDiscGolf.Profiles
{
    public class PlayerCardProfile : Profile
    {
        public PlayerCardProfile()
        {
            CreateMap<PlayerCardViewModel, PlayerCard>();
            CreateMap<PlayerCard, PlayerCardViewModel>();

        }
    }
}
