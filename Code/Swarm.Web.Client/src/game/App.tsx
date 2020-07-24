import React from 'react';
import TileElement from './TileElement';
import { Tile, TileColor, ICoOrds } from './domain';
import { Button } from 'antd';
import * as signalR from "@microsoft/signalr";

export interface IAppState {
  tiles: Array<Tile>;
  window: any;
  gameEdges: Array<Tile>;
}

export default class App extends React.Component<any, IAppState> {
  constructor(props: any) {
    super(props);
    this.state = {
      tiles: [
        new Tile (0, 0, TileColor.Green),
        new Tile (1, 1, TileColor.Orange),
        new Tile (1, -1, TileColor.Green),
        new Tile (0, -2, TileColor.Orange),
        new Tile (1, 3, TileColor.Orange),
        new Tile (1, 5, TileColor.Orange)
      ],
      window: { width: 0, height: 0 },
      gameEdges: []
    };
    this.updateWindowDimensions = this.updateWindowDimensions.bind(this);
  }

  componentDidMount() {
    this.updateWindowDimensions();
    window.addEventListener('resize', this.updateWindowDimensions);
    const hubConnection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5000/hub").build();

  }

  componentWillUnmount() {
    window.removeEventListener('resize', this.updateWindowDimensions);
  }

  updateWindowDimensions() {
    this.setState({window: { width: window.innerWidth, height: window.innerHeight }});
  }

  calcEdges() {
    let allEdges: Array<ICoOrds> = [];
    this.state.tiles.forEach(tile => allEdges = allEdges.concat(tile.edges));
    let distinctEdges: Array<ICoOrds> = [...new Set(allEdges)];
    let unassignedDistinctEdges = distinctEdges.filter(edge => !this.state.tiles.some(tile => tile.x === edge.x && tile.y === edge.y));
    this.setState({gameEdges: unassignedDistinctEdges.map(edge => new Tile(edge.x, edge.y, TileColor.Unassigned))});
  }

  render() {
    let tileElements = this.state.tiles.map((tile, index) => <TileElement key={index} data={tile} window={this.state.window}></TileElement>)
    let tileEdges = this.state.gameEdges.map((tile, index) => <TileElement key={index} data={tile} window={this.state.window}></TileElement>)

    return (
      <div>
        <Button type="primary" onClick={this.calcEdges.bind(this)}>Calculate Edges</Button>
        {tileElements}
        {tileEdges}
      </div>
    )
  };
}