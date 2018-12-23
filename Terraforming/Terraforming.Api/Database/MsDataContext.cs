using Microsoft.EntityFrameworkCore;
using Terraforming.Api.Models;

namespace Terraforming.Api.Database
{
    public class MsDataContext : DbContext
    {
        public MsDataContext(DbContextOptions<MsDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<GameScore> GameScore { get; set; }
        public DbSet<Game> Game { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
