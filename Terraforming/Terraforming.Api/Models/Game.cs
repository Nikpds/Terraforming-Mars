using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Terraforming.Api.Models
{
    public class Game : Entity
    {
        public DateTime Date { get; set; }
        public int NoOfPlayers { get; set; }
        public string Board { get; set; }

        public List<GamePlayer> GamePlayers { get; set; }

        public Game()
        {
            GamePlayers = new List<GamePlayer>();
        }
    }
}
