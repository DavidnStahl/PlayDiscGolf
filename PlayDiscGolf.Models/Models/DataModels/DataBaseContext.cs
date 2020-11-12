using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlayDiscGolf.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models
{
    public class DataBaseContext : IdentityDbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Hole> Holes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ScoreCard> ScoreCards { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<HoleCard> HoleCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here..
            base.OnModelCreating(modelBuilder);
        }
    }
}
