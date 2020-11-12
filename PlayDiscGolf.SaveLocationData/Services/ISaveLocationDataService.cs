using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.SaveLocationData.Services
{
    public interface ISaveLocationDataService
    {
        public List<Location> AddValidLocationFromRoot(Root root);
        public Root ReadLocationDataToRoot();
        public void SaveLocationsToDataBase(List<Location> locations);
    }
}
