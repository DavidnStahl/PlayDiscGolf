using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.Models.ViewModels;

namespace PlayDiscGolf.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {

            //Source -> Target
            CreateMap<Location, SearchLocationItemViewModel>();
        }
    }
}
