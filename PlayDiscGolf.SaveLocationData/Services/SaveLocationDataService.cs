using Newtonsoft.Json;
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlayDiscGolf.SaveLocationData.Services
{
    class SaveLocationDataService : ISaveLocationDataService
    {
        private readonly DataBaseContext _context = new DataBaseContext();
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

        public void SaveLocationsToDataBase(List<Location> locations)
        {
            _context.Locations.AddRange(locations);
            _context.SaveChanges();
        }
    }
}
