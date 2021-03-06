﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Models.Models;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.SaveLocationData
{
    public class SaveLocationDataService : ISaveLocationDataService
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IHttpContextAccessor _accessor;

        public SaveLocationDataService(IUnitOfwork unitOfwork, IHttpContextAccessor accessor)
        {
            _unitOfwork = unitOfwork;
            _accessor = accessor;
        }
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
                    course.Enddate == null &&
                    !string.IsNullOrWhiteSpace(course.X) &&
                    !string.IsNullOrWhiteSpace(course.Y))
                {
                    var validLocaction = new Course
                    {
                        CourseID = Guid.NewGuid(),
                        Name = course.Name,
                        ApiID = course.ID,
                        ApiParentID = (string)course.ParentID,
                        CountryCode = course.CountryCode,
                        Country = "Sweden",
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
            using (StreamReader read = new StreamReader("LocationData.json"))
            {
                string json = read.ReadToEnd();
                root = JsonConvert.DeserializeObject<Root>(json);
            }

            return root;
        }

        public void SaveLocationsToDataBase(List<Course> courses)
        {
            _unitOfwork.Courses.AddRange(courses);
            _unitOfwork.Complete();
        }
    }
}
