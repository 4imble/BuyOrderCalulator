using System;

namespace Swarm.Domain
{
    public class PlayerModel
    {
        public Guid Id { get; internal set; }
        public PlayerColour Colour { get; internal set; }
        public string Name { get; internal set; }
    }
}