import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import SetName from './SetName';
import { useCookies } from 'react-cookie';
import * as signalR from "@microsoft/signalr";
import { Game } from '../game/domain';
import { Card, Button, Row, Col } from 'antd';

export default function Lobby(props: any) {
    const history = useHistory();
    const [cookie, setCookie, removeCookie] = useCookies(['swarm']);
    const [name, setName] = useState("");
    const [games, setGames] = useState<Array<Game>>([]);
    const [hubConnection, setHubConnection] = useState<signalR.HubConnection>();

    useEffect(() => { if (cookie.swarm) setName(cookie.swarm.name) }, []);

    useEffect(() => {
        const createHubConnection = async () => {
            const hubConnect = new signalR.HubConnectionBuilder()
                .withUrl('/gamehub')
                .build();
            try {
                await hubConnect.start()
                console.log('Connection successful!')
            }
            catch (err) {
                alert(err);
            }
            setHubConnection(hubConnect);

            await hubConnect.invoke("JoinLobby");

            hubConnect.on("addGame", (gameId) => addGame(gameId));
        }

        createHubConnection();
    }, []);

    const addGame = (gameId: string) => {
        setGames(games => [...games, new Game(gameId)])
    }

    let gameRows = games.map((game, index) => <div key={index} >{game.id} <Button onClick={() => history.push("/Game/"+game.id)} type="primary">View</Button><hr /></div>)

    return (
        <Row>
            <Col className="gutter-row" flex="auto"></Col>
            <Col className="gutter-row">
                <SetName name={name} setName={setName}></SetName>
                <Card title="Games" style={{ marginTop: '20px', width: '300px' }}>
                    <Button hidden={!cookie.swarm?.name} style={{ marginTop: '7px' }} type="primary" onClick={() => hubConnection?.invoke("CreateGame")} block>Create Game</Button>
                    {gameRows}
                </Card>
            </Col>
            <Col className="gutter-row" flex="auto"></Col>
        </Row >
    )
}