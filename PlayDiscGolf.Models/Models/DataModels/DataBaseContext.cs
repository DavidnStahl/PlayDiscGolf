using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.Models.DataModels
{
    public class DataBaseContext : IdentityDbContext
    {
        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }

        public DbSet<Hole> Holes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ScoreCard> ScoreCards { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<HoleCard> HoleCards { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-CEEMT81\\SQLEXPRESS;Initial Catalog=PlayDiscGolfDB;Integrated Security=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here..
            base.OnModelCreating(modelBuilder);
        }
    }
}
