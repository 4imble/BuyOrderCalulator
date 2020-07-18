import React from 'react';
import Tile from './Tile';
import { ITile, TileColor } from './interfaces';

export interface IAppState {
  tiles: Array<ITile>;
  window: any;
}

export default class App extends React.Component<any, IAppState> {
  constructor(props: any) {
    super(props);
    this.state = {
      tiles: [
        { x: 0, y: 0, color: TileColor.Green },
        { x: 1, y: 1, color: TileColor.Orange },
        { x: 1, y: -1, color: TileColor.Green },
        { x: 0, y: -2, color: TileColor.Orange }
      ],
      window: { width: 0, height: 0 }
    };
    this.updateWindowDimensions = this.updateWindowDimensions.bind(this);
  }

  componentDidMount() {
    this.updateWindowDimensions();
    window.addEventListener('resize', this.updateWindowDimensions);
  }

  componentWillUnmount() {
    window.removeEventListener('resize', this.updateWindowDimensions);
  }

  updateWindowDimensions() {
    this.setState({window: { width: window.innerWidth, height: window.innerHeight }});
  }

  render() {
    let tileElements = this.state.tiles.map((tile, index) => <Tile key={index} data={tile} window={this.state.window}></Tile>)

    return (
      <div>
        {tileElements}
      </div>
    )
  };
}