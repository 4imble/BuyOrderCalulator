import React from 'react';
import './TileElement.less'
import { Tile, TileColor } from './domain';

export interface ITileProps {
  data: Tile,
  window: any
}

export default function TileElement(props: ITileProps) {
  const left = (): number => {
    return (props.data.x * 77) + (props.window.width / 2) - 50;
  }

  const top = (): number => {
    return (props.data.y * 45) + (props.window.height / 2) - 45;
  }

  const color = (): string => {
    if (props.data.color === TileColor.Unassigned)
      return "unassigned";
    return props.data.color ? "green" : "orange";
  }

  return (
    <div className={'tile'} style={{ top: top(), left: left() }}>
      <img alt="tile" className={color()} src={require('../../images/tile.png')}></img>
      <div className="coords">[{props.data.x} / {props.data.y}]</div>
    </div>
  )
}