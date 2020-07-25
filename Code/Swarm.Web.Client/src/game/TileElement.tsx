import React from 'react';
import './TileElement.less'
import { Tile, TileColor } from './domain';

export interface ITileProps {
  data: Tile,
  window: any
}

export default class TileElement extends React.Component<ITileProps, any> {
  constructor(public props: ITileProps) {
    super(props);
  }

  get left(): number {
    return (this.props.data.x * 77) + (this.props.window.width / 2) - 50;
  }

  get top(): number {
    return (this.props.data.y * 45) + (this.props.window.height / 2) - 45;
  }

  get color(): string {
    if(this.props.data.color === TileColor.Unassigned)
      return "unassigned";
    return this.props.data.color ? "green" : "orange";
  }

  render() {
    return (
      <div className={'tile'} style={{ top: this.top, left: this.left }}>
        <img alt="tile" className={this.color} src={require('../images/tile.png')}></img>
        <div className="coords">[{this.props.data.x} / {this.props.data.y}]</div>
      </div>
    )
  };
}