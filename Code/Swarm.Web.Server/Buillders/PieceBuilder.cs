using Swarm.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Swarm.Web.Server.Buillders
{
    public static class PieceBuilder
    {
        public static PieceModel BuildForView(this Piece piece)
        {
            return new PieceModel
            {
                PlayerId = piece.Player.Id,
                Type = piece.Type,
                XPos = piece.XPos,
                YPos = piece.YPos
            };
        }

        public static List<PieceModel> BuildForView(this IEnumerable<Piece> pieces)
        {
            return pieces.Select(x => x.BuildForView()).ToList();
        }
    }
}
