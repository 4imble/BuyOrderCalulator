using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Swarm.Domain;
using Swarm.EntityFramework;
using Swarm.Web.Server.Buillders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swarm.Web.Server
{
    public class GameHub : Hub
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
            var newGame = GenerateNewGame();
            dataContext.Games.Add(newGame);
            dataContext.SaveChanges();

            await Clients.Group("Lobby").SendAsync("addGame", newGame.Id);
        }

        public async Task JoinGame(Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game_" + gameId.ToString());
            var game = dataContext.Games
                .Include(x => x.Player1)
                .Include(x => x.Player2)
                .Include(x => x.Pieces)
                .Single(x => x.Id == gameId);

            var gameModel = game.BuildForView();
            await Clients.Group("Game_" + gameId.ToString()).SendAsync("joinGame", game);
        }

        public async Task LeaveGame(Guid gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Game_" + gameId.ToString());
        }

        private static Game GenerateNewGame()
        {
            var game = new Game();
            game.Pieces.AddRange(GeneratePlayerPieces(game, game.Player1));
            game.Pieces.AddRange(GeneratePlayerPieces(game, game.Player2));
            return game;
        }

        private static List<Piece> GeneratePlayerPieces(Game game, Player player)
        {
            return new List<Piece> {
                new Piece() { Player = player, Type = PieceType.Ant },
                new Piece() { Player = player, Type = PieceType.Ant },
                new Piece() { Player = player, Type = PieceType.Ant },
                new Piece() { Player = player, Type = PieceType.Grasshopper },
                new Piece() { Player = player, Type = PieceType.Grasshopper },
                new Piece() { Player = player, Type = PieceType.Grasshopper },
                new Piece() { Player = player, Type = PieceType.Beetle },
                new Piece() { Player = player, Type = PieceType.Beetle },
                new Piece() { Player = player, Type = PieceType.Spider },
                new Piece() { Player = player, Type = PieceType.Spider },
                new Piece() { Player = player, Type = PieceType.Bee },
                new Piece() { Player = player, Type = PieceType.Mosquito },
                new Piece() { Player = player, Type = PieceType.Ladybug },
                new Piece() { Player = player, Type = PieceType.Pillbug },
            };
        }
    }
}
