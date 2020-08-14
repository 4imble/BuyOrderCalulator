import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import TileElement from './TileElement';
import GamePlayer from './GamePlayer';
import { Tile, ICoOrds, Game, Player } from './domain';
import { useCookies } from 'react-cookie';
import * as signalR from "@microsoft/signalr";
import { plainToClass } from 'class-transformer';
import './GamePage.less'

export default function GamePage(props: any) {
  const history = useHistory();
  const [cookie, setCookie, removeCookie] = useCookies(['swarm']);
  const [tiles, setTiles] = useState<Array<Tile>>([]);
  const [players, setPlayers] = useState<Array<Player>>([new Player(), new Player()]);
  const [windowProps, setWindowProps] = useState({ width: 0, height: 0 });
  const [gameEdges, setGameEdges] = useState<Array<Tile>>([]);
  const [hubConnection, setHubConnection] = useState<signalR.HubConnection>();

  const calcEdges = () => {
    let allEdges: Array<ICoOrds> = [];
    tiles.forEach(tile => allEdges = allEdges.concat(tile.edges));
    let distinctEdges: Array<ICoOrds> = [...new Set(allEdges)];
    let unassignedDistinctEdges = distinctEdges.filter(edge => !tiles.some(tile => tile.x === edge.x && tile.y === edge.y));

    if (!unassignedDistinctEdges.length)
      unassignedDistinctEdges = [{ x: 0, y: 0 }];

    setGameEdges(unassignedDistinctEdges.map(edge => new Tile(edge.x, edge.y)));
  }

  useEffect(() => {
    const createHubConnection = async () => {
      const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('/gamehub')
        .build();
      try {
        await hubConnection.start()
        console.log('Connection successful!')
      }
      catch (err) {
        alert(err);
      }
      setHubConnection(hubConnection);
      hubConnection?.invoke("JoinGame", props.match.params.id);
      hubConnection.on("joinGame", (game) => joinGame(game));
    }

    createHubConnection();
    return () => {
      hubConnection?.invoke("LeaveLobby");
      hubConnection?.stop();
    }
  }, []);

  const joinGame = (game: Game) => {
    let piecesAsTiles = game.pieces.map(piece => {
      return new Tile(piece.x, piece.y);
    })

    let player1 = plainToClass(Player, game.player1);
    let player2 = plainToClass(Player, game.player2);

    setTiles(piecesAsTiles);
    setPlayers([player1, player2]);
  }

  useEffect(() => {
    if (!cookie.swarm?.name)
      history.push("/");
  })

  useEffect(() => {
    updateWindowDimensions();
    window.addEventListener('resize', updateWindowDimensions);
    return () => window.removeEventListener('resize', updateWindowDimensions);
  }, []);

  useEffect(calcEdges, []);

  const updateWindowDimensions = () => {
    setWindowProps({ width: window.innerWidth, height: window.innerHeight });
  }

  const takeSeat = async (playerId: string) => {
    await hubConnection?.invoke("ClaimPlayer", props.match.params.id, playerId, cookie.swarm.id, cookie.swarm.name);
    await hubConnection?.invoke("JoinGame", props.match.params.id);
  }

  let tileElements = tiles.filter(tile => tile.x !== undefined && tile.y != undefined).map((tile, index) => <TileElement key={index} data={tile} window={windowProps}></TileElement>)
  let tileEdges = gameEdges.map((tile, index) => <TileElement key={index} data={tile} window={windowProps}></TileElement>)

  return (
    <div>
      {tileElements}
      {tileEdges}

      <GamePlayer takeSeat={takeSeat} player={players[0]}></GamePlayer>
      <GamePlayer takeSeat={takeSeat} player={players[1]} color={'orange'} isActive={true}></GamePlayer>
      <div className="gameId">{props.match.params.id}</div>
    </div>
  )
}