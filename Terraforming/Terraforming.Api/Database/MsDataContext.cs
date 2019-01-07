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
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var invitatiton = modelBuilder.Entity<Invitation>();
            invitatiton.HasKey(x => x.Id);
            invitatiton.HasOne(x => x.User)
                       .WithMany(x => x.Invitations)
                       .HasForeignKey(k => k.UserId)
                       .OnDelete(DeleteBehavior.Restrict);
            invitatiton.HasOne(x => x.Owner)
                     .WithMany(x => x.Invites)
                     .HasForeignKey(k => k.OwnerId)
                     .OnDelete(DeleteBehavior.Restrict);


            var user = modelBuilder.Entity<User>();
            user.HasKey(x => x.Id);
            user.Property(p => p.Firstname).IsRequired();
            user.Property(p => p.Email).IsRequired();
            user.Property(p => p.Lastname).IsRequired();
            user.HasMany(x => x.Invitations)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            user.HasMany(x => x.Invites)
               .WithOne(x => x.Owner)
               .HasForeignKey(x => x.OwnerId)
               .OnDelete(DeleteBehavior.Restrict);

            var teamUser = modelBuilder.Entity<TeamUsers>();
            teamUser.HasOne(x => x.User).WithMany(w => w.TeamUsers)
                .HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            teamUser.HasOne(x => x.Team).WithMany(w => w.TeamUsers)
                .HasForeignKey(f => f.TeamId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
