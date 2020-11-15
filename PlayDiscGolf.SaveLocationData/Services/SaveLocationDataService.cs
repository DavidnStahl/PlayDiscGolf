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
        public List<Course> AddValidLocationFromRoot(Root root)
        {
            var locations = new List<Course>();

            foreach (var course in root.Courses)
            {
                if (!string.IsNullOrWhiteSpace(course.Fullname) && 
                    !string.IsNullOrWhiteSpace(course.X) && 
                    !string.IsNullOrWhiteSpace(course.Y) && 
                    !string.IsNullOrWhiteSpace(course.ID) && 
                    !string.IsNullOrWhiteSpace(course.Name) &&
                    !string.IsNullOrWhiteSpace(course.Area) &&
                    !string.IsNullOrWhiteSpace(course.ID) &&
                    course.Enddate == null)
                {
                    var validLocaction = new Course
                    {
                        CourseID = Guid.NewGuid(),
                        Name = course.Name,
                        ApiID = course.ID,
                        ApiParentID = (string)course.ParentID,
                        CountryCode = course.CountryCode,
                        Area = course.Area,
                        FullName = course.Fullname,
                        Main = course.ParentID == null ? true : false,
                        Latitude = course.X,
                        Longitude = course.Y
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

        public async Task SaveLocationsToDataBase(List<Course> courses)
        {           
            var _context = new DataBaseContext();
            await _context.Courses.AddRangeAsync(courses);
            await _context.SaveChangesAsync();
        }
    }
}
