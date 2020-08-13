using System;

namespace Swarm.Domain
{
    public class PieceModel
    {
        public Guid PlayerId { get; internal set; }
        public PieceType Type { get; internal set; }
        public int? XPos { get; internal set; }
        public int? YPos { get; internal set; }
    }
}