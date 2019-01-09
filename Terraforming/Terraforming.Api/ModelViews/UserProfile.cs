using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Models;

namespace Terraforming.Api.ModelViews
{
    public class UserProfile
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<Team> Teams { get; set; }

        public UserProfile()
        {
            Teams = new HashSet<Team>();
            Invitations = new HashSet<Invitation>();
        }

        public UserProfile(User u)
        {
            Firstname = u.Firstname;
            Lastname = u.Lastname;
            Nickname = u.Nickname;
            Email = u.Email;
            Teams = new HashSet<Team>();
            Invitations = new HashSet<Invitation>();
        }
    }
}
