using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlayDiscGolf.Models;

namespace PlayDiscGolf.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBaseContext _context;
        static HttpClient client = new HttpClient();

        public HomeController(DataBaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async void SaveApiData()
        {
            var path = "https://discgolfmetrix.com/api.php?content=courses_list&country_code=SE";
            using (WebClient wc = new WebClient())
            {
                JsonReader json = wc.DownloadString(path);
                Root myDeserializedClass = JsonSerializer.Deserialize<Root>(json);
            }

            

        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
