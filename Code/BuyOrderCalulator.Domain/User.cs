using System;
using System.Collections.Generic;
using System.Text;

namespace BuyOrderCalc.Domain
{
    public class User: Entity
    {
        public string DiscordId { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Discriminator { get; set; }
        public bool IsAdmin { get; set; }
        public string AccessToken { get; set; }

        public string AvatarLink => $"https://cdn.discordapp.com/avatars/{DiscordId}/{Avatar}.jpg";

        public DateTime TokenExpires { get; set; }
    }
}
