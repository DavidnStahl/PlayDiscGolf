using PlayDiscGolf.SaveLocationData.Services;
using System.Threading.Tasks;

namespace PlayDiscGolf.SaveLocationData
{
    class Program
    {
        private static readonly ISaveLocationDataService _saveLocationDataService = new SaveLocationDataService();
        static async Task Main(string[] args)
        {
            var root = _saveLocationDataService.ReadLocationDataToRoot();
            var locations = _saveLocationDataService.AddValidLocationFromRoot(root);
            await _saveLocationDataService.SaveLocationsToDataBase(locations);
        }      
    }
}
