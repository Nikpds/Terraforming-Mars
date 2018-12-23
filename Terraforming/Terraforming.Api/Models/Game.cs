using System;
using System.Collections.Generic;

namespace Terraforming.Api.Models
{
    public class Game : Entity
    {
        public DateTime Date { get; set; }
        public List<GameScore> GamePlayers { get; set; }

        public Game()
        {
            GamePlayers = new List<GameScore>();
        }
    }
}
