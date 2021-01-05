using AutoMapper;
using PlayDiscGolf.Core.Dtos.Account;
using PlayDiscGolf.Models.ViewModels.Account;

namespace PlayDiscGolf.AutoMapper.Profiles.Account
{
    public class LoginProfiles : Profile
    {
        public LoginProfiles()
        {
            CreateMap<LoginViewModel, LoginDto>();
            CreateMap<LoginDto, LoginViewModel>();
        }
    }
}
