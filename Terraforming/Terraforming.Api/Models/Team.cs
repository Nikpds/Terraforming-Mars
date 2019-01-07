using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Terraforming.Api.Models
{
    public class Team : Entity
    {
        public string Title { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<TeamUsers> TeamUsers { get; set; }
        public virtual ICollection<Invitation> Invations { get; set; }

        public Team()
        {
            TeamUsers = new HashSet<TeamUsers>();
        }
    }

    public class TeamUsers : Entity
    {
        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class Invitation : Entity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public string TeamId { get; set; }
        public string TeamTitle { get; set; }
        public string Comments { get; set; }
        public DateTime Created { get; set; }
        public InvitationStatus InivtationStatus { get; set; }
        public DateTime ActionDate { get; set; }

    }

    public enum InvitationStatus
    {
        Pending,
        Accepted,
        Declined
    }
}
