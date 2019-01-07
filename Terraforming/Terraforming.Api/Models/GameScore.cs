using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Terraforming.Api.Models
{
    public class GameScore : Entity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string GameId { get; set; }
        public virtual Game Game { get; set; }

        public int Points { get; set; }
        public int Place { get; set; }
        public int AwardsPlaced { get; set; }
        public int AwardsWon { get; set; }
        public int Milestones { get; set; }
        public BoardMats Board { get; set; }
    }

    public enum BoardMats
    {
        Basic,
        Hellas,
        Elysium
    }
}
