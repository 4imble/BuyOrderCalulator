using Swarm.Domain;

namespace Swarm.Web.Server.Buillders
{
    public static class PlayerBuilder
    {
        public static PlayerModel BuildForView(this Player player)
        {
            return new PlayerModel
            {
                Id = player.Id,
                Colour = player.Colour,
                Name = player.Name
            };
        }
    }
}
