using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Terraforming.Api.Models
{
    public class User : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool ExternaLogin { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public string VerificationToken { get; set; }
        public virtual ICollection<TeamUser> TeamUsers { get; set; }
        public virtual ICollection<GameScore> GameScores { get; set; }
        public UserRole UserRole { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Invitation> Invitations { get; set; }
        [InverseProperty("Owner")]
        public virtual ICollection<Invitation> Invites { get; set; }

        public User()
        {
            TeamUsers = new HashSet<TeamUser>();
            GameScores = new HashSet<GameScore>();
            Invitations = new HashSet<Invitation>();
            Invites = new HashSet<Invitation>();
        }
    }

    public enum UserRole
    {
        Admin,
        User,
        GM
    }
}
