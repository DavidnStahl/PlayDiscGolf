﻿using PlayDiscGolf.Models.DataModels;
using System.Collections.Generic;


namespace PlayDiscGolf.SaveLocationData.Services
{
    public interface ISaveLocationDataService
    {
        public List<Location> AddValidLocationFromRoot(Root root);
        public Root ReadLocationDataToRoot();
        public void SaveLocationsToDataBase(List<Location> locations);
    }
}
