import React from "react";
import { Avatar, Badge } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import './PlayerName.less';

export default class TileElement extends React.Component {
    constructor(public props: any) {
        super(props);
    }

    render() {
        return (
            <div className="playerName">
                <span>
                    <Badge dot={this.props.isActive}>
                        <img alt="tile" className={this.props.color} src={require('../images/tile.png')}></img>
                    </Badge>
                    <span className="name">{this.props.name}</span>
                </span>
            </div>)
    }
}