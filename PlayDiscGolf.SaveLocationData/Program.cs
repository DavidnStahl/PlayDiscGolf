using PlayDiscGolf.SaveLocationData.Services;


namespace PlayDiscGolf.SaveLocationData
{
    class Program
    {
        private static readonly ISaveLocationDataService _saveLocationDataService = new SaveLocationDataService();
        static void Main(string[] args)
        {
            var root = _saveLocationDataService.ReadLocationDataToRoot();
            var locations = _saveLocationDataService.AddValidLocationFromRoot(root);
            _saveLocationDataService.SaveLocationsToDataBase(locations);
        }      
    }
}
