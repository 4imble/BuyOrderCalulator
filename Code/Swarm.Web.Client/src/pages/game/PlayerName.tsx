import React from "react";
import { Badge } from 'antd';
import './PlayerName.less';

export default function PlayerName(props: any) {
    return (
        <div className="playerName">
            <span>
                <Badge dot={props.isActive}>
                    <img alt="tile" className={props.color} src={require('../../images/tile.png')}></img>
                </Badge>
                <span className="name">{props.name}</span>
            </span>
        </div>)
}