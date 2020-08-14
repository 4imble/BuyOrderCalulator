import React from "react";
import { Badge, Button } from 'antd';
import './GamePlayer.less';

export default function GamePlayer(props: any) {
    let player = <div className="gamePlayer">
        <span>
            <Badge dot={props.isActive}>
                <img alt="tile" className={props.player.colour} src={require('../../images/tile.png')}></img>
            </Badge>
            <span className="name">{props.player.name}</span>
        </span>
    </div>

    let seat = <div className="gamePlayer">
        <span>
            <Badge>
                <img alt="tile" src={require('../../images/tile.png')}></img>
            </Badge>
            <span className="name"><Button onClick={() => props.takeSeat(props.player.id)} type="primary" size="small">Take Seat</Button></span>
        </span>
    </div>

    return props.player.name !== null ? player : seat;
}