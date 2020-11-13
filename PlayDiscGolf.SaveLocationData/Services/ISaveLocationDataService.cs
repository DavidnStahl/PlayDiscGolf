using PlayDiscGolf.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.SaveLocationData.Services
{
    public interface ISaveLocationDataService
    {
        public List<Location> AddValidLocationFromRoot(Root root);
        public Root ReadLocationDataToRoot();
        public Task SaveLocationsToDataBase(List<Location> locations);
    }
}
