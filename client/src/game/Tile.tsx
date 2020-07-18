import React from 'react';
import './Tile.less'
import { ITile } from './interfaces';

export interface ITileProps
{
  data: ITile,
  window: any
}

export default class Tile extends React.Component<ITileProps, any> {
  constructor(public props: ITileProps) {
    super(props);
  }

  get left(): number {
    return (this.props.data.x * 77) + (this.props.window.width /2)-50;
  } 

  get top(): number {
    return (this.props.data.y * 44) + (this.props.window.height /2)-44;
  }

  get color(): string
  {
    return this.props.data.color ? "green" : "orange";
  }

  render() {
    return (
      <div className={'tile'} style={{ top: this.top, left: this.left }}>
        <img className={this.color}  src={require('../images/tile.png')}></img>
    <div className="coords">[{this.props.data.x} / {this.props.data.y}]</div>
      </div>
    )
  };
}