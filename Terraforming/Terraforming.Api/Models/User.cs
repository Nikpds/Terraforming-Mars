using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Terraforming.Api.Models
{
    public class User : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool ExternaLogin { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public string VerificationToken { get; set; }
        public List<Team> Teams { get; set; }
        public List<GameScore> GameScores { get; set; }

        public User()
        {
            Teams = new List<Team>();
            GameScores = new List<GameScore>();
        }
    }
}
