using Newtonsoft.Json;
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.SaveLocationData.Services
{
    class SaveLocationDataService : ISaveLocationDataService
    {
        public List<Location> AddValidLocationFromRoot(Root root)
        {
            var locations = new List<Location>();

            foreach (var course in root.Courses)
            {
                if (!string.IsNullOrWhiteSpace(course.Fullname) &&
                    !string.IsNullOrWhiteSpace(course.X) &&
                    !string.IsNullOrWhiteSpace(course.Y) &&
                    !string.IsNullOrWhiteSpace(course.ID) &&
                    course.ParentID == null && course.Enddate == null &&
                    !locations.Select(c => c.Latitude).Contains(Convert.ToDecimal(course.X)))
                {
                    var validLocaction = new Location
                    {
                        LocationID = Guid.NewGuid(),
                        Name = course.Fullname,
                        Latitude = Convert.ToDecimal(course.X),
                        Longitude = Convert.ToDecimal(course.Y)
                    };

                    locations.Add(validLocaction);
                }
            }
            return locations;
        }

        public Root ReadLocationDataToRoot()
        {
            var root = new Root();

            using (StreamReader read = new StreamReader($@"C:\Source\Examenarbete\PlayDiscGolf\PlayDiscGolf.SaveLocationData\LocationData.json"))
            {
                string json = read.ReadToEnd();
                root = JsonConvert.DeserializeObject<Root>(json);
            }

            return root;
        }

        public async Task SaveLocationsToDataBase(List<Location> locations)
        {           
            var _context = new DataBaseContext();
            await _context.AddRangeAsync(locations);
            await _context.SaveChangesAsync();
        }
    }
}
