using System;

namespace Swarm.Domain
{
    public class Player : Entity
    {
        public Guid ClientGuid { get; set; }
        public PlayerColour Colour { get; set; }
        public string Name { get; set; }
    }
}
