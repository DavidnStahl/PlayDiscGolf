using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace PlayDiscGolf.Models.Models.DataModels
{
    public class DataBaseContext : IdentityDbContext
    {

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<Hole> Holes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ScoreCard> ScoreCards { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<HoleCard> HoleCards { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here..
            base.OnModelCreating(modelBuilder);
        }
    }
}
