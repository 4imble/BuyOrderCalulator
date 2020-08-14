using System;
using System.Collections.Generic;

namespace Swarm.Domain
{
    public class GameModel
    {
        public PlayerModel Player1 { get; internal set; }
        public PlayerModel Player2 { get; internal set; }
        public List<PieceModel> Pieces { get; internal set; }
        public Guid Id { get; internal set; }
    }
}