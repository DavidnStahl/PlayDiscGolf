using PlayDiscGolf.Models.Models;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.SaveLocationData
{
    public interface ISaveLocationDataService
    {
        public List<Course> AddValidLocationFromRoot(Root root);
        public Root ReadLocationDataToRoot();
        public void SaveLocationsToDataBase(List<Course> locations);
    }
}
