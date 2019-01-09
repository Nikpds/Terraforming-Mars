using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Models;

namespace Terraforming.Api.ModelViews
{
    public class TeamView
    {
        public string Title { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string Onwer { get; set; }
        public virtual ICollection<TeamMembers> Members { get; set; }
    }

    public class TeamMembers
    {
        public string FullName { get; set; }
        public DateTime Joined { get; set; }
    }

}
