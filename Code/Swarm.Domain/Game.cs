using System;
using System.Collections.Generic;

namespace Swarm.Domain
{
    public class Game: Entity
    {
        public Player Player1 { get; set; } = new Player();
        public Player Player2 { get; set; } = new Player();
        public List<Piece> Pieces { get; set; } = new List<Piece>();
    }
}
