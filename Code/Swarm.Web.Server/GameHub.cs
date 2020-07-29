using Microsoft.AspNetCore.SignalR;
using Swarm.Domain;
using Swarm.EntityFramework;
using System.Threading.Tasks;

namespace Swarm.Web.Server
{
    public class GameHub: Hub
    {
        private readonly DataContext dataContext;

        public GameHub(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task JoinLobby()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Lobby");
        }

        public async Task LeaveLobby()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Lobby");
        }

        public async Task CreateGame()
        {
            var newGame = new Game();
            dataContext.Games.Add(newGame);
            dataContext.SaveChanges();

            await Clients.Group("Lobby").SendAsync("addGame", newGame.Id);
        }
    }
}
