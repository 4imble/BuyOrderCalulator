namespace Swarm.Domain
{
    public class Piece: Entity
    {
        public Player Player { get; set; }
        public PieceType Type { get; set; }
        public int? XPos { get; set; }
        public int? YPos { get; set; }
    }
}