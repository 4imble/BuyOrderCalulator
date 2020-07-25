import React from 'react';
import TileElement from './TileElement';
import PlayerName from './PlayerName';
import { Tile, TileColor, ICoOrds } from './domain';
// import * as signalR from "@microsoft/signalr";

export interface IGameState {
  tiles: Array<Tile>;
  window: any;
  gameEdges: Array<Tile>;
}

export default class Game extends React.Component<any, IGameState> {
  constructor(props: any) {
    super(props);
    this.state = {
      tiles: [],
      window: { width: 0, height: 0 },
      gameEdges: []
    };
    this.updateWindowDimensions = this.updateWindowDimensions.bind(this);
  }

  componentDidMount() {
    this.updateWindowDimensions();
    window.addEventListener('resize', this.updateWindowDimensions);
    this.calcEdges()
    // const hubConnection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5000/hub").build();
  }

  componentWillUnmount() {
    window.removeEventListener('resize', this.updateWindowDimensions);
  }

  updateWindowDimensions() {
    this.setState({ window: { width: window.innerWidth, height: window.innerHeight } });
  }

  calcEdges() {
    let allEdges: Array<ICoOrds> = [];
    this.state.tiles.forEach(tile => allEdges = allEdges.concat(tile.edges));
    let distinctEdges: Array<ICoOrds> = [...new Set(allEdges)];
    let unassignedDistinctEdges = distinctEdges.filter(edge => !this.state.tiles.some(tile => tile.x === edge.x && tile.y === edge.y));

    if (!unassignedDistinctEdges.length)
       unassignedDistinctEdges = [{ x: 0, y: 0 }];

    this.setState({ gameEdges: unassignedDistinctEdges.map(edge => new Tile(edge.x, edge.y, TileColor.Unassigned)) });
  }

  render() {
    let tileElements = this.state.tiles.map((tile, index) => <TileElement key={index} data={tile} window={this.state.window}></TileElement>)
    let tileEdges = this.state.gameEdges.map((tile, index) => <TileElement key={index} data={tile} window={this.state.window}></TileElement>)

    return (
      <div>
        {tileElements}
        {tileEdges}
        <PlayerName name={'Player 1'} color={'green'}></PlayerName>
        <PlayerName name={'Player 2'} color={'orange'} isActive={true}></PlayerName>
      </div>
    )
  };
}