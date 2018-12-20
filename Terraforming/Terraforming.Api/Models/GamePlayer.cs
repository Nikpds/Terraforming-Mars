using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Terraforming.Api.Models
{
    public class GamePlayer
    {
        public string Player { get; set; }
        public string PlayerId { get; set; }
        public int Points { get; set; }
        public int Place { get; set; }
        public int AwardsPlaced { get; set; }
        public int AwardsWon { get; set; }
        public int Milestones { get; set; }
    }
}
