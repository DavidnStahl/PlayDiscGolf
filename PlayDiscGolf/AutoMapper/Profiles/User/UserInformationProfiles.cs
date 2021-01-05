using AutoMapper;
using PlayDiscGolf.Core.Dtos.User;
using PlayDiscGolf.ViewModels.User;

namespace PlayDiscGolf.AutoMapper.Profiles.User
{
    public class UserInformationProfiles : Profile
    {
        public UserInformationProfiles()
        {
            CreateMap<UserInformationViewModel, UserInformationDto>();
            CreateMap<UserInformationDto, UserInformationViewModel>();
        }
    }
}
