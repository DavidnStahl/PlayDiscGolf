using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Core.Services.SaveLocationData;

namespace PlayDiscGolf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var iHost = CreateHostBuilder(args).Build();
            InitializeDb(iHost);
            iHost.Run();
        }

        private static void InitializeDb(IHost iHost)
        {
            using (var scope = iHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataBaseContext>();
                var db = new DatabaseInitializer();
                db.Initialize(context);
                db.InitializePlayDiscGolf(context, services.GetRequiredService<UserManager<IdentityUser>>());
                CheckIfLocationDataNeedToBeSeeded(context, services.GetRequiredService<ISaveLocationDataService>());
            }
        }

        private static void CheckIfLocationDataNeedToBeSeeded(DataBaseContext context, ISaveLocationDataService saveLocationDataService)
        {
            var courses = context.Courses.Any(x => x.Country == "Sweden");
            if(!courses)
            {
                var root = saveLocationDataService.ReadLocationDataToRoot();
                var locations = saveLocationDataService.AddValidLocationFromRoot(root);
                saveLocationDataService.SaveLocationsToDataBase(locations);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
