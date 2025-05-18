namespace BEBChampionship.Data
{
    using BEBChampionship.Models;
    using Microsoft.EntityFrameworkCore;

    public class BEBContext : DbContext
    {
        public DbSet<GolfCourse> GolfCourses { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }

        public BEBContext(DbContextOptions<BEBContext> options) : base(options) { }
    }
}
