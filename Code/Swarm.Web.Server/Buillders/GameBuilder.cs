using Swarm.Domain;

namespace Swarm.Web.Server.Buillders
{
    public static class GameBuilder
    {
        public static GameModel BuildForView(this Game game)
        {
            return new GameModel
            {
                Id = game.Id,
                Player1 = game.Player1.BuildForView(),
                Player2 = game.Player2.BuildForView(),
                Pieces = game.Pieces.BuildForView()
            };
        }
    }
}