using BuyOrderCalc.Domain;
using BuyOrderCalc.EntityFramework;
using BuyOrderCalc.Web.Server.Controllers;
using RestSharp;
using System;
using System.Linq;

namespace BuyOrderCalc.Web.Server.Helpers
{
    public class AuthHelper
    {
        private readonly DataContext dataContext;

        public AuthHelper(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        private User CreateNewUser(UserResponse discordUserDetails)
        {
            var user = new User
            {
                DiscordId = discordUserDetails.id,
                Avatar = discordUserDetails.avatar,
                Username = discordUserDetails.username,
                Discriminator = discordUserDetails.discriminator
            };

            dataContext.Users.Add(user);
            return user;
        }

        private static UserResponse GetUserFromDiscord(string access_token)
        {
            var client = new RestClient("https://discord.com/api/users/@me");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("authorization", "Bearer " + access_token);
            var response = client.Execute<UserResponse>(request);
            return response.Data;
        }

        public User SetUser(AuthController.TokenResponse data)
        {
            var discordUserDetails = GetUserFromDiscord(data.access_token);
            var user = dataContext.Users.SingleOrDefault(x => x.DiscordId == discordUserDetails.id);

            if (user == null)
            {
                user = CreateNewUser(discordUserDetails);
            }

            user.AccessToken = data.access_token;
            user.TokenExpires = DateTime.Now.AddMinutes(55);

            dataContext.SaveChanges();

            return user;
        }
    }

    public class UserResponse
    {
        //"{\"id\":\"x\",\"username\":\"x\",\"avatar\":\"x\",\"discriminator\":\"x\",\"public_flags\":0,\"flags\":0,\"locale\":\"x\",\"mfa_enabled\":true,\"premium_type\":2}"
        public string id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string discriminator { get; set; }
        public string public_flags { get; set; }
        public string flags { get; set; }
        public string locale { get; set; }
        public string mfa_enabled { get; set; }
        public string premium_type { get; set; }
    }
}
