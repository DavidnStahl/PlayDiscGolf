using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PlayDiscGolf.Models.DataModels;
using PlayDiscGolf.SaveLocationData.Services;
using PlayDiscGolf.Services.SaveLocationData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
